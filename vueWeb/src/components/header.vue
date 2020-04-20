<template>
  <div>
    <!--导航条-->
    <el-menu
      class="el-menu-demo"
      mode="horizontal"
      background-color="#545c64"
      text-color="#fff"
      active-text-color="#ffd04b"
    >
      <el-menu-item index="1">
        <router-link to="/listBlog">处理中心</router-link>
      </el-menu-item>
      <el-menu-item index="2">
        <a href="https://github.com/ZhengHengWU" target="_blank">我的工作台</a>
      </el-menu-item>
      <el-menu-item index="3">
        <a href="https://www.cnblogs.com/ZhengHengWU" target="_blank">关于我们</a>
      </el-menu-item>
    </el-menu>
    <div class="header-logout">
      <a href="#" @click.prevent="LoginOut">退出</a>
    </div>
  </div>
</template>
<script>
export default {
  data() {
    return {};
  },
  created() {},
  methods: {
    LoginOut() {
      //退出功能
      //弹出确认对话框
      //用户点击确认，跳回用户登录页面，清除token
      this.$confirm("确定要退出登录吗?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          this.$message({
            type: "success",
            message: "退出成功!"
          });
          this.$router.push("/");
        })
        .catch(() => {
          this.$message({
            type: "info",
            message: "已取消退出"
          });
        });
    }
  }
};
</script>

<style>
.el-carousel__item h3 {
  color: #475669;
  font-size: 14px;
  opacity: 0.75;
  line-height: 200px;
  margin: 0;
}

.el-carousel__item:nth-child(2n) {
  background-color: #99a9bf;
}

.el-carousel__item:nth-child(2n + 1) {
  background-color: #d3dce6;
}
.el-menu-demo {
  margin-left: -25px;
}
.header-logout a {
  font-size: 14px;
  color: #67c23a;
}
.header-logout {
  position: absolute;
  margin-left: 93%;
  margin-top: -2.1%;
}
</style>
