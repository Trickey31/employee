import { createApp } from "vue";
import App from "./App.vue";
import vformat from "./commons/vformat";
import vresource from "./commons/vresource";
import venum from "./commons/venum";
import vcommon from "./commons/vcommon";
import vueRouter from "./router";
import emitter from "tiny-emitter/instance";
import visvalid from "./commons/visvalid";

const app = createApp(App);

app.config.globalProperties.$_vformat = vformat;
app.config.globalProperties.$_venum = venum;
app.config.globalProperties.$_vresource = vresource;
app.config.globalProperties.$_vcommon = vcommon;
app.config.globalProperties.$_visvalid = visvalid;
app.config.globalProperties.$_emitter = emitter;

app.config.globalProperties.$employee = vcommon["Employee"];
app.config.globalProperties.$status = venum.Status;
app.config.globalProperties.$vndialogTitles =
  vresource["VN"]["dialog"]["titles"];
app.config.globalProperties.$vndialogMessages =
  vresource["VN"]["dialog"]["messages"];
app.config.globalProperties.$vnbutton = vresource["VN"]["buttons"];
app.config.globalProperties.$vnform = vresource["VN"]["form"];

app.use(vueRouter);
app.mount("#app");

export default app;
