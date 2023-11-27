// import venum from "./venum";
// import vresource from "./vresource";

const visvalid = {
  /**
   * Định dạng email
   * Author: Vương Tiến Thành (4/9/2023)
   */
  isEmail(email) {
    const emailReg = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    return emailReg.test(email);
  },

  /**
   * Định dạng phoneNumber
   * Author: Vương Tiến Thành (4/9/2023)
   */
  isPhoneNumber(phoneNumber) {
    const phoneNumberReg = /^\d{10,}$/;
    return phoneNumberReg.test(phoneNumber);
  },

  /**
   * Định dạng date
   * Author: Vương Tiến Thành (4/9/2023)
   */
  isDate(date) {
    const day = new Date(date);
    const today = new Date();

    return day < today;
  },
};
export default visvalid;
