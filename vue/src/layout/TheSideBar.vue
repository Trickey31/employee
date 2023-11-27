<template>
  <div class="sidebar">
    <div class="sidebar-title">
      <div class="icon__wrapper">
        <VIcon :class="'icon__show'"></VIcon>
      </div>
      <div class="logo">
        <img
          src="../assets/img/Logo_Module_TiengViet_White.66947422.svg"
          alt="logo"
        />
      </div>
    </div>
    <ul class="sidebar-menu">
      <router-link to="/" class="sidebar__item">
        <VIcon :class="'icon-sidebar-dashboard'"></VIcon>
        <span>Tổng quan</span>
      </router-link>
      <router-link to="/option" class="sidebar__item">
        <VIcon :class="'icon-sidebar-cash'"></VIcon>
        <span>Tùy chọn</span>
      </router-link>
      <router-link to="/bill" class="sidebar__item">
        <VIcon :class="'icon-sidebar-bank'"></VIcon>
        <span>Tiền gửi</span>
      </router-link>
      <router-link to="/bill" class="sidebar__item">
        <VIcon :class="'icon-sidebar-pu'"></VIcon>
        <span>Mua hàng</span>
      </router-link>
      <router-link to="/bill" class="sidebar__item">
        <VIcon :class="'icon-sidebar-sale'"></VIcon>
        <span>Bán hàng</span>
      </router-link>
      <router-link to="/bill" class="sidebar__item">
        <VIcon :class="'icon-sidebar-invoice'"></VIcon>
        <span>Quản lý hóa đơn</span>
      </router-link>
      <router-link to="/bill" class="sidebar__item">
        <VIcon :class="'icon-sidebar-stock'"></VIcon>
        <span>Kho</span>
      </router-link>
      <router-link to="/bill" class="sidebar__item">
        <VIcon :class="'icon-sidebar-tools'"></VIcon>
        <span>Công cụ dụng cụ</span>
      </router-link>
      <router-link to="/bill" class="sidebar__item">
        <VIcon :class="'icon-sidebar-assets'"></VIcon>
        <span>Tài sản cố định</span>
      </router-link>
      <router-link to="/bill" class="sidebar__item">
        <VIcon :class="'icon-sidebar-tax'"></VIcon>
        <span>Thuế</span>
      </router-link>
      <router-link to="/bill" class="sidebar__item">
        <VIcon :class="'icon-sidebar-price'"></VIcon>
        <span>Giá thành</span>
      </router-link>
      <router-link to="/bill" class="sidebar__item">
        <VIcon :class="'icon-sidebar-general'"></VIcon>
        <span>Tổng hợp</span>
      </router-link>
      <router-link to="/bill" class="sidebar__item">
        <VIcon :class="'icon-sidebar-budget'"></VIcon>
        <span>Ngân sách</span>
      </router-link>
      <router-link to="/bill" class="sidebar__item">
        <VIcon :class="'icon-sidebar-report'"></VIcon>
        <span>Báo cáo</span>
      </router-link>
      <router-link to="/bill" class="sidebar__item">
        <VIcon :class="'icon-sidebar-finance'"></VIcon>
        <span>Phân tích tài chính</span>
      </router-link>
    </ul>
  </div>
</template>
<script>
import VIcon from "@/components/VIcon.vue";
export default {
  name: "TheSideBar",
  components: {
    VIcon,
  },
};
</script>
<style>
@import url("../css/layout/sidebar.css");
</style>
