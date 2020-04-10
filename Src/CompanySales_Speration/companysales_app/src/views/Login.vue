<template>
  <el-container class="fill-container">
    <el-form
      class="login-container"
      ref="loginForm"
      :model="loginForm"
      :label-position="labelPosition"
      label-width="80px"
    >
      <el-form-item label="用户名：">
        <el-input v-model="loginForm.userid" placeholder="用户ID"></el-input>
      </el-form-item>
      <el-form-item label="密码：">
        <el-input type="password" v-model="loginForm.password" placeholder="password"></el-input>
      </el-form-item>
      <el-form-item>
        <el-button class="full-width" type="primary" @click="loginHandler">登录</el-button>
      </el-form-item>
      <el-form-item>
        <el-link type>没有账号，点我注册</el-link>
      </el-form-item>
    </el-form>
  </el-container>
</template>

<script>
export default {
  data() {
    return {
      loginForm: {
        userid: "",
        password: ""
      },
      // 表单标签顶部对齐
      labelPosition: "top"
    };
  },
  methods: {
    loginHandler() {
      let sdata = this.$data.loginForm;
      this.axios
        .get(`/Security/Login?uid=${sdata.userid}&pwd=${sdata.password}`)
        .then(res => {
          // 验证通过
          if (res.data.status) {
            // 路由跳转
            this.$router.push({ name: "Home" });
          } else {
            this.$message.error("用户名或密码错误");
          }
        })
        .catch(err => {
          console.error(err);
        });
    }
  }
};
</script>

<style>
/* 导入样式 */
@import "../styles/login-part.css";
</style>