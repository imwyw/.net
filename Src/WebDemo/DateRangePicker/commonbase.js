/**
 * 基于 http://www.daterangepicker.com/ 控件使用jQuery的中文封装
 * add by wangyuanwei 2017-12-26
 * http://www.cnblogs.com/zcynine/p/5028207.html
 */

$.fn.dateRangerPicerCH = function (opt) {
    var defaultOptions = {
        "locale": {
            "direction": "ltr",
            //"format": "YYYY-MM-DD HH:mm",
            "format": "YYYY-MM-DD",
            "separator": " - ",
            "applyLabel": "确认",
            "cancelLabel": "取消",
            "fromLabel": "从",
            "toLabel": "至",
            "customRangeLabel": "Custom",
            "daysOfWeek": [
                "周日",
                "周一",
                "周二",
                "周三",
                "周四",
                "周五",
                "周六"
            ],
            "monthNames": [
                "一月",
                "二月",
                "三月",
                "四月",
                "五月",
                "六月",
                "七月",
                "八月",
                "九月",
                "十月",
                "十一月",
                "十二月"
            ],
            "firstDay": 1
        }
    };
    opt = $.extend({}, defaultOptions, opt);
    this.daterangepicker(opt);
}