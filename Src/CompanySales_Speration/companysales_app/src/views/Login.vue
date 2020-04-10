<template>
  <el-form class="login-container" ref="loginForm" :model="loginForm" label-width="80px">
    <el-form-item label="用户名：">
      <el-input v-model="loginForm.userid"></el-input>
    </el-form-item>
    <el-form-item label="密码：">
      <el-input type="password" v-model="loginForm.password"></el-input>
    </el-form-item>
    <el-alert v-show="isError" title="用户名或密码错误" type="error"></el-alert>
    <el-form-item>
      <el-button type="primary" @click="loginHandler">登录</el-button>
      <el-link type="primary">没有账号，点我注册</el-link>
    </el-form-item>
  </el-form>
</template>

<script>
export default {
  data() {
    return {
      loginForm: {
        userid: "",
        password: ""
      },
      isError: false
    };
  },
  methods: {
    loginHandler() {
      let sdata = this.$data.loginForm;
      this.axios
        .get(`/Security/Login?uid=${sdata.userid}&pwd=${sdata.password}`)
        .then(res => {
          if (res.data.status) {
            this.$router.push({ name: "Home" });
          } else {
            this.$data.isError = true;
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
.login-container {
  width: 400px;
}
</style>