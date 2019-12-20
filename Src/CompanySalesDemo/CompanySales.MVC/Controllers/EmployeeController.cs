using CompanySales.BLL;
using CompanySales.Model;
using CompanySales.Model.Domain;
using CompanySales.Model.Entity;
using CompanySales.Model.Parameter;
using CompanySales.MVC.Base;
using CompanySales.MVC.Core;
using CompanySales.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompanySales.MVC.Controllers
{
    /// <summary>
    /// 组织架构相关控制器
    /// 包括员工、部门等
    /// </summary>
    [CustomActionFilter]
    public class EmployeeController : BaseController
    {
        /// <summary>
        /// 部门组织结构
        /// </summary>
        /// <returns></returns>
        public ActionResult DepartmentList()
        {
            return View();
        }

        /// <summary>
        /// 获取绑定 树形组件 结构数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetDeptEmpData()
        {
            var treeData = GetDepartmentTreeData();

            return Json(treeData);
        }

        /// <summary>
        /// 获取所有部门数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetDepartmentData()
        {
            var res = DepartmentMgr.GetList();
            return Json(res);
        }

        /// <summary>
        /// 分页获取员工数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetEmployeeData(DeptEmpParameter parameter)
        {
            Pager<DeptEmpInfo> res = DepartmentMgr.GetDeptEmpInfo(parameter);
            return Json(res);
        }

        /// <summary>
        /// 转换list数据为树形组件绑定结构
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<LayTreeItem> GetDepartmentTreeData()
        {
            List<LayTreeItem> res = new List<LayTreeItem>();
            List<Department> depList = DepartmentMgr.GetList();
            List<Employee> empList = EmployeeMgr.GetList();

            // 第一次遍历，插入一级部门
            foreach (var item in depList)
            {
                var node = new LayTreeItem();
                node.title = item.DepartmentName;
                node.field = item.DepartmentID.ToString();
                node.id = $"dep_{item.DepartmentID.ToString()}";
                res.Add(node);
            }
            // 第二次遍历，在部门下插入员工
            foreach (var item in res)
            {
                // 找到对应部门下所有员工
                var emps = empList.Where(t => t.DepartmentID.ToString() == item.field);
                foreach (var empItem in emps)
                {
                    var empNode = new LayTreeItem();
                    empNode.title = empItem.EmployeeName;
                    empNode.field = empItem.EmployeeID.ToString();
                    empNode.id = $"emp_{empItem.EmployeeID.ToString()}";

                    item.children.Add(empNode);
                }
            }

            return res;
        }
    }
}