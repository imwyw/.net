<template>
  <el-container direction="vertical">
    <h1>产品明细</h1>
    <el-form :model="prod">
      <el-form-item label>
        <el-date-picker type="datetime" v-model="prod.date1" placeholder></el-date-picker>
      </el-form-item>
    </el-form>
    <hr />
    <h4>{{prod.date1.toLocaleString()}}</h4>

    <h3>上传文件</h3>
    <el-upload
      class="upload-demo"
      ref="upload"
      action="http://localhost:5000/api/Test/OnPostUpload?testid=123"
      :auto-upload="false"
    >
      <el-button slot="trigger" size="small" type="primary">选取文件</el-button>
      <el-button style="margin-left: 10px;" size="small" type="success" @click="submitUpload">上传到服务器</el-button>
    </el-upload>
  </el-container>
</template>

<script>
export default {
  data() {
    return {
      prod: {
        date1: ""
      },
    };
  },
  methods: {
    submitUpload() {
      this.$refs.upload.submit();
    }
  }
};
</script>

<style>
</style>