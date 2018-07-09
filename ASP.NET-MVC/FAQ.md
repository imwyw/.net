<!-- TOC -->

- [疑难杂症](#疑难杂症)
    - [RenderAction 多级嵌套](#renderaction-多级嵌套)

<!-- /TOC -->

# 疑难杂症

## RenderAction 多级嵌套
三个razor语法页面A.cshtml、B.cshtml、C.cshtml，A的layout是B，B的layout是C 

只能C的命名以"_Layout"开头，如果B也是以"_Layout"开头就会报找不到section

DTOrgnView.cshtml的Layout为_FlowViewListLayout.cshtml， 
_FlowViewListLayout.cshtml的Layout为_Layout.cshtml 
比如下图： 

![](..\assets\asp.net-mvc\multi-level-layout.png)

