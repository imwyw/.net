using CompanySales.BLL;
using CompanySales.Model;
using CompanySales.Model.Entity;
using CompanySales.MVC.Base;
using CompanySales.MVC.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompanySales.MVC.Controllers
{
    [CustomActionFilter]
    public class AttachmentController : BaseController
    {
        /// <summary>
        /// 根据id删除附件资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult RemoveAttachById(int id)
        {
            StateModel res = new StateModel();
            res.Status = AttachmentMgr.DeleteById(id);
            return Json(res);
        }

        /// <summary>
        /// 根据路径获取图片
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public FileResult GetImageByPath(string fullPath)
        {
            string mime = MimeMapping.GetMimeMapping(fullPath);
            return File(fullPath, mime);
        }

        /// <summary>
        /// 获取关联id对应的资源集合
        /// </summary>
        /// <param name="relatedId">关联资源主键ID</param>
        /// <returns></returns>
        public JsonResult GetListByRelatedId(int relatedId)
        {
            List<Attachment> res = AttachmentMgr.GetListByRelatedId(relatedId);
            return Json(res);
        }

        /// <summary>
        /// 根据路径获取视频
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public FileStreamResult GetVideoByPath(string fullPath)
        {
            string mime = MimeMapping.GetMimeMapping(fullPath);
            FileStream fs = new FileStream(fullPath, FileMode.Open);
            return new FileStreamResult(fs, mime);
        }

        /// <summary>
        /// https://archive.codeplex.com/?p=videostreamer
        /// https://blogs.visigo.com/chriscoulson/easy-handling-of-http-range-requests-in-asp-net/
        /// 处理Http Range Requests，分段获取，支持视频的快进后退播放
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public EmptyResult GetVideoByRange(string fullPath)
        {
            //context.Response.Headers.Clear();
            string mimetype = MimeMapping.GetMimeMapping(fullPath);

            if (System.IO.File.Exists(fullPath))
            {
                Response.ContentType = mimetype;
                if (!string.IsNullOrEmpty(Request.ServerVariables["HTTP_RANGE"]))
                {
                    //request for chunk
                    RangeDownload(fullPath);
                }
                else
                {
                    //ask for all 
                    long fileLength = System.IO.File.OpenRead(fullPath).Length;
                    Response.AddHeader("Content-Length", fileLength.ToString());
                    Response.WriteFile(fullPath);
                }
            }
            else
            {
                throw new HttpException(404, "Video Not Found Path:" + fullPath);
            }
            return new EmptyResult();
        }


        private void RangeDownload(string fullpath)
        {
            long size, start, end, length, fp = 0;
            using (StreamReader reader = new StreamReader(fullpath))
            {
                size = reader.BaseStream.Length;
                start = 0;
                end = size - 1;
                length = size;
                // Now that we've gotten so far without errors we send the accept range header
                /* At the moment we only support single ranges.
                 * Multiple ranges requires some more work to ensure it works correctly
                 * and comply with the spesifications: http://www.w3.org/Protocols/rfc2616/rfc2616-sec19.html#sec19.2
                 *
                 * Multirange support annouces itself with:
                 * header('Accept-Ranges: bytes');
                 *
                 * Multirange content must be sent with multipart/byteranges mediatype,
                 * (mediatype = mimetype)
                 * as well as a boundry header to indicate the various chunks of data.
                 */
                Response.AddHeader("Accept-Ranges", "0-" + size);
                // header('Accept-Ranges: bytes');
                // multipart/byteranges
                // http://www.w3.org/Protocols/rfc2616/rfc2616-sec19.html#sec19.2

                if (!string.IsNullOrEmpty(Request.ServerVariables["HTTP_RANGE"]))
                {
                    long anotherStart = start;
                    long anotherEnd = end;
                    string[] arr_split = Request.ServerVariables["HTTP_RANGE"].Split(new char[] { Convert.ToChar("=") });
                    string range = arr_split[1];

                    // Make sure the client hasn't sent us a multibyte range
                    if (range.IndexOf(",") > -1)
                    {
                        // (?) Shoud this be issued here, or should the first
                        // range be used? Or should the header be ignored and
                        // we output the whole content?
                        Response.AddHeader("Content-Range", "bytes " + start + "-" + end + "/" + size);
                        throw new HttpException(416, "Requested Range Not Satisfiable");
                    }

                    // If the range starts with an '-' we start from the beginning
                    // If not, we forward the file pointer
                    // And make sure to get the end byte if spesified
                    if (range.StartsWith("-"))
                    {
                        // The n-number of the last bytes is requested
                        anotherStart = size - Convert.ToInt64(range.Substring(1));
                    }
                    else
                    {
                        arr_split = range.Split(new char[] { Convert.ToChar("-") });
                        anotherStart = Convert.ToInt64(arr_split[0]);
                        long temp = 0;
                        anotherEnd = (arr_split.Length > 1 && Int64.TryParse(arr_split[1].ToString(), out temp)) ? Convert.ToInt64(arr_split[1]) : size;
                    }
                    /* Check the range and make sure it's treated according to the specs.
                     * http://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html
                     */
                    // End bytes can not be larger than $end.
                    anotherEnd = (anotherEnd > end) ? end : anotherEnd;
                    // Validate the requested range and return an error if it's not correct.
                    if (anotherStart > anotherEnd || anotherStart > size - 1 || anotherEnd >= size)
                    {

                        Response.AddHeader("Content-Range", "bytes " + start + "-" + end + "/" + size);
                        throw new HttpException(416, "Requested Range Not Satisfiable");
                    }
                    start = anotherStart;
                    end = anotherEnd;

                    length = end - start + 1; // Calculate new content length
                    fp = reader.BaseStream.Seek(start, SeekOrigin.Begin);
                    Response.StatusCode = 206;
                }
            }
            // Notify the client the byte range we'll be outputting
            Response.AddHeader("Content-Range", "bytes " + start + "-" + end + "/" + size);
            Response.AddHeader("Content-Length", length.ToString());
            // Start buffered download
            Response.WriteFile(fullpath, fp, length);
            Response.End();

        }
    }
}