<template>
  <div>
    <el-header class="header">
      <!-- 头部组件渲染 -->
      <Header></Header>
    </el-header>
    <br />
    <el-form ref="form" label-width="80px" :model="form">
      <el-form-item label="标题">
        <el-input v-model="form.title"></el-input>
      </el-form-item>
      <el-form-item label="链接">
        <el-input v-model="form.link"></el-input>
      </el-form-item>
      <el-form-item label="作者">
        <el-input v-model="form.author"></el-input>
      </el-form-item>
      <el-form-item label="显隐">
        <el-select v-model="form.tag" placeholder="请选择">
          <el-option label="显示" value="1"></el-option>
          <el-option label="隐藏" value="0"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" @click="SaveBlog">保存</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>
<script>
import Header from "./header.vue";
import { Toast } from 'vant';
export default {
  created() {
    var id = this.$route.params.id;
    if (id != undefined) {
      this.$get("Blog/GetBlogById?id=" + this.$route.params.id).then(result => {
        if (result.state == 1) {
          if (result.data != null) {
            this.form = result.data;
          }else{
            this.$message.error('获取失败!');
          }
        }
      });
    }
  },
  components: {
    Header
  },
  data() {
    return {
      form: {
        id: 0,
        title: "",
        link: "",
        author: "",
        tag: ""
      }
    };
  },
  methods: {
    SaveBlog() {
      this.$post("Blog/SaveBlog", this.form).then(result => {
        if (result.state == 1) {
          this.form = {
            id: "",
            title: "",
            link: "",
            author: "",
            tag: ""
          };
          this.$router.push({
            path: "/listBlog"
          });
        } else {
          this.$message.error('保存失败!');
        }
      });
    }
  }
};
</script>

<style>
</style>
