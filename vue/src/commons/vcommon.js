import emitter from "tiny-emitter/instance";

const vcommon = {
  /**
   * Xử lí sự kiện showPoup
   * Author: VTThanh (10/9/2023)
   */
  showPopup() {
    emitter.emit("onShowPopup");
  },

  /**
   * Xử lí sự kiện closePopup
   * Author: VTThanh (10/9/2023)
   */
  closePopup() {
    emitter.emit("onClosePopup");
  },

  /**
   * Xử lí sự kiện showToast
   * Author: VTThanh (02/08/2023)
   */
  showToast(msg) {
    emitter.emit("onShowToast", msg);
  },
  /**
   * Xử lí sự kiện closeToast
   * Author: VTThanh (02/08/2023)
   */
  closeToast() {
    emitter.emit("onCloseToast");
  },
  /**
   * Xử lí sự kiện showDialog
   * Author: VTThanh (02/08/2023)
   */
  showDialog(title, desc) {
    emitter.emit("onShowDialog", title, desc);
  },
  /**
   * Xử lí sự kiện closeDialog
   * Author: VTThanh (02/08/2023)
   */
  closeDialog() {
    emitter.emit("onCloseDialog");
  },

  /**
   * Xử lí sự kiện showLoading
   * Author: VTThanh (21/08/2023)
   */
  showLoading() {
    emitter.emit("onShowLoading");
  },
  /**
   * Xử lí sự kiện closeLoading
   * Author: VTThanh (21/08/2023)
   */
  closeLoading() {
    emitter.emit("onCloseLoading");
  },
};
export default vcommon;
