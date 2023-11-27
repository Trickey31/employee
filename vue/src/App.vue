<template>
  <TheSideBar></TheSideBar>
  <div class="main">
    <TheHeader></TheHeader>
    <TheContent></TheContent>
  </div>
</template>

<script>
import TheHeader from "./layout/TheHeader.vue";
import TheSideBar from "./layout/TheSideBar.vue";
import TheContent from "./layout/TheContent.vue";
export default {
  name: "App",
  components: {
    TheSideBar,
    TheHeader,
    TheContent,
  },
};
</script>

<style>
@import url("./css/main.css");
#app {
  display: flex;
}
.main {
  width: calc(100vw - 200px);
}
</style>
