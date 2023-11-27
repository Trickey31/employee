<template>
  <div class="header">
    <div class="header__company">
      <div class="icon__wrapper">
        <i class="icon icon-three-stripes"></i>
      </div>
      <h3 class="header__name">
        CÔNG TY TNHH SẢN XUẤT - THƯƠNG MẠI - SẢN XUẤT QIU PHÚC
      </h3>
      <div class="icon__wrapper">
        <i class="icon icon-chevron-down"></i>
      </div>
    </div>
    <div class="header__employee">
      <div class="icon__wrapper">
        <i class="icon icon-bell"></i>
      </div>
      <div class="header__profile">
        <img
          src="https://images.unsplash.com/photo-1524504388940-b1c1722653e1?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTd8fHBlcnNvbm5lc3xlbnwwfHwwfHx8MA%3D%3D&auto=format&fit=crop&w=500&q=60"
          alt="avatar"
          class="header__avatar"
        />
        <span>Vương Tiến Thành</span>
      </div>
      <div class="icon__wrapper">
        <i class="icon icon__arrow--down"></i>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "TheHeader",
};
</script>
<style>
@import url("../css/layout/header.css");
</style>
