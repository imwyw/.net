<template>
  <el-container class="fill-container">
    <el-form
      class="login-container"
      ref="refLogin"
      :model="loginForm"
      :label-position="labelPosition"
      :rules="rules"
      label-width="80px"
    >
      <el-form-item label="用户名：" prop="userid">
        <el-input v-model="loginForm.userid" placeholder="用户ID"></el-input>
      </el-form-item>
      <el-form-item label="密码：" prop="password">
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
      labelPosition: "top",
      // 表单验证规则
      rules: {
        // 规则必须与data属性对应，el-form-item的prop属性保持一致
        userid: [
          { required: true, message: "用户名不可为空", trigger: "blur" }
        ],
        password: [
          { required: true, message: "密码不可为空", trigger: "blur" },
          { min: 2, message: "密码太短", trigger: "blur" }
        ]
      }
    };
  },
  methods: {
    loginHandler() {
      let sdata = this.$data.loginForm;
      // 提交时需要验证规则
      this.$refs["refLogin"].validate(valid => {
        // 验证通过方可提交
        if (valid) {
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
        } else {
          this.$message.error("信息填写有误！");
        }
      });
    }
  }
};
</script>

<style>
/* 导入样式 */
@import "../styles/login-part.css";
</style>
