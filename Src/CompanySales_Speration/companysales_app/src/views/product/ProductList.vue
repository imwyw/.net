<template>
  <el-container direction="vertical">
    <el-header>产品列表</el-header>
    <el-table :data="productList">
      <el-table-column label="名称" prop="ProductName"></el-table-column>
      <el-table-column label="价格" prop="Price"></el-table-column>
      <el-table-column label="库存" prop="ProductStockNumber"></el-table-column>
      <el-table-column label="已售数量" prop="ProductSellNumber"></el-table-column>
    </el-table>
  </el-container>
</template>

<script>
export default {
  data() {
    return {
      productList: []
    };
  },
  mounted() {
    this.axios
      .post("/Product/GetList", {})
      .then(res => {
        console.log(res);
        this.productList = res.data.Rows;
      })
      .catch(err => {
        console.error(err);
      });
  }
};
</script>

<style>
</style>