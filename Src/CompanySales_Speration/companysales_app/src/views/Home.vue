<template>
  <el-container>
    <el-header>
      <!-- 网站图标标题等 -->
      <h1>
        <el-link :underline="false" @click="switchCollapse">CompanySales</el-link>
      </h1>
      <!-- 一级导航，大菜单 -->
      <el-menu class="main-menu" mode="horizontal" default-active="1">
        <el-menu-item index="1">控制台</el-menu-item>
        <el-menu-item index="2">初始化表结构</el-menu-item>
        <el-menu-item index="3">控制台</el-menu-item>
      </el-menu>
      <!-- 用户相关设置，头像等等 -->
      <el-menu class="config-menu" mode="horizontal" default-active="1">
        <el-menu-item index="1">用户</el-menu-item>
      </el-menu>
    </el-header>
    <el-container>
      <!-- 侧边栏，二级导航 -->
      <el-aside>
        <el-menu
          :router="true"
          default-active="2-1"
          class="el-menu-vertical"
          @open="handleOpen"
          @close="handleClose"
          :collapse="isCollapse"
        >
          <el-submenu index="1">
            <template slot="title">订单</template>
            <el-menu-item index="1-1">销售订单</el-menu-item>
            <el-menu-item index="1-2">采购订单</el-menu-item>
          </el-submenu>
          <el-submenu index="2">
            <template slot="title">产品管理</template>
            <el-menu-item index="2-1" :route="{name:'ProductList'}">产品列表</el-menu-item>
            <el-menu-item index="2-2" :route="{name:'ProductDetail'}">新增产品</el-menu-item>
          </el-submenu>
        </el-menu>
      </el-aside>
      <el-main>
        <router-view></router-view>
      </el-main>
    </el-container>
  </el-container>
</template>

<script>
export default {
  data() {
    return {
      isCollapse: true
    };
  },
  methods: {
    switchCollapse() {
      this.isCollapse = !this.isCollapse;
    },
    handleOpen(key, keyPath) {
      // 菜单展开
      console.log(key, keyPath);
    },
    handleClose(key, keyPath) {
      // 菜单收缩
      console.log(key, keyPath);
    }
  }
};
</script>

<style>
/* 主页样式，设计非常简单 */
.el-container {
  height: 100%;
}
.el-header {
  padding: 0;
  border-bottom: 1px solid #dcdfe6;
}
/* 图标样式 */
.el-header h1 {
  float: left;
  margin: 0;
  padding: 5px;
}
.el-header h1 .el-link {
  font-size: 30px;
  font-weight: 500;
}
.el-header .main-menu {
  float: left;
}
.el-header .config-menu {
  float: right;
  margin-right: 50px;
}
/* 纵向导航 展开宽度 */
.el-menu-vertical:not(.el-menu--collapse) {
  width: 200px;
}
/* 纵向导航 收缩宽度 */
.el-menu--collapse {
  width: 90px;
}
</style>