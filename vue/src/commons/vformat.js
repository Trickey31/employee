import venum from "./venum";

const format = {
  /**
   * Format ngày sinh
   * Author: Vương Tiến Thành (21/8/2023)
   */
  formatDate(dateString, format) {
    if (dateString == null || dateString == "") {
      return null;
    }
    //1. Chuyển chuỗi thành định dạng ngày tháng
    const date = new Date(dateString);
    //2. Trích xuất ngày, tháng, năm từ đối tượng ngày tháng
    const day = String(date.getDate()).padStart(2, "0");
    const month = String(date.getMonth() + 1).padStart(2, "0");
    const year = String(date.getFullYear());
    //3. Xây dựng chuỗi dd/mm/yyyy
    let formattedDate = "";
    switch (format) {
      case "dd/MM/yyyy":
        formattedDate = `${day}/${month}/${year}`;
        break;
      case "yyyy-MM-dd":
        formattedDate = `${year}-${month}-${day}`;
        break;
      case "dd-MM-yyyy":
        formattedDate = `${day}-${month}-${year}`;
        break;
      default:
        formattedDate = null;
        break;
    }

    if (formattedDate == "01/01/1" || formattedDate == "1-01-01") {
      return null;
    }

    return formattedDate;
  },

  /**
   * Format giới tính
   * Author: Vương Tiến Thành (21/8/2023)
   */
  formatGender(genderValue) {
    try {
      if (genderValue === venum["Gender"].Male) {
        return "Nam";
      } else if (genderValue === venum["Gender"].Female) {
        return "Nữ";
      } else {
        return "Khác";
      }
    } catch (error) {
      console.log(error);
      return "Khác";
    }
  },
};
export default format;
