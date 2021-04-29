<template>
  <div>
    <div
      id="dlgCustomerDetail"
      class="dialog "
      :class="{ 'dialog-hide': !isShow }"
    >
      <div class="model"></div>
      <div class="dialog-content">
        <div class="dialog-header">
          <div class="dialog-title">THÔNG TIN KHÁCH HÀNG</div>
          <div class="dialog-close-button" @click="btnCloseOnClick()">
            &#x2715;
          </div>
        </div>
        <div class="dialog-body">
          <div class="m-row">
            <div class="m-col">
              <label>Mã khách hàng</label>
              <input
                id="txtCustomerCode"
                type="text"
                v-model="customer.customerCode"
              />
            </div>
            <div class="m-col">
              <label>Họ và tên</label>
              <input id="txtFullName" type="text" v-model="customer.fullName" />
            </div>
          </div>
          <div class="m-row">
            <div class="m-col">
              <label>Nhóm khách hàng</label>
              <select id="cbCustomerGroup" v-model="customer.customerGroupId">
                <option value="19165ed7-212e-21c4-0428-030d4265475f"
                  >Nhóm khách hàng MISA</option
                >
                <option value="7a0b757e-41eb-4df6-c6f8-494a84b910f4"
                  >Khách VIP</option
                >
                <option value="2924c34d-68f1-1d0a-c9c7-6c0aeb6ec302"
                  >Khách vãng lai</option
                >
                <option value="3631011e-4559-4ad8-b0ad-cb989f2177da"
                  >Khách thường</option
                >
              </select>
            </div>
          </div>
          <div class="m-row">
            <div class="m-col">
              <label>Giới tính</label>
              <select id="cbGender" v-model="customer.gender">
                <option value="1">Nam</option>
                <option value="2">Nữ</option>
                <option value="0">Không xác định</option>
              </select>
            </div>
            <div class="m-col">
              <label>Ngày sinh</label>
              <input
                id="dtDateOfBirth"
                type="date"
                v-bind:value="fomatBirthday(customer.dateOfBirth)"
                v-on:input="customer.dateOfBirth = $event.target.value"
              />
              />
            </div>
          </div>
          <div class="m-row">
            <div class="m-col">
              <label>Số điện thoại</label>
              <input
                id="txtPhoneNumber"
                type="text"
                v-model="customer.phoneNumber"
              />
            </div>
            <div class="m-col">
              <label>Email</label>
              <input id="txtEmail" type="text" v-model="customer.email" />
            </div>
          </div>
        </div>
        <div class="dialog-footer">
          <button id="btnSave" class="btn-default" @click="btnSaveOnClick()">
            Lưu
          </button>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import axios from "axios";
import dayjs from "dayjs";
export default {
  methods: {
    btnCloseOnClick() {
      this.$emit("hideDialog");
      document.getElementById("txtCustomerCode").value = "";
    },
    fomatBirthday(cusbirthday) {
      if (cusbirthday == null) {
        return 0;
      }
      return dayjs(cusbirthday).format("YYYY-MM-DD");
    },
    btnSaveOnClick() {
      if (this.formMode == "add") {
        axios
          .post("https://localhost:44321/api/v1/Customers", this.customer)
          .then(() => {
            console.log(this.customer);
            alert("Thêm thành công");
            this.$emit("hideDialog");
            this.$emit("loadData");
          })
          .catch((res) => {
            console.log(this.customer);
            console.log(res);
          });
      } else {
        axios
          .put(
            "https://localhost:44321/api/v1/Customers/" +
              this.customer.customerId,
            this.customer
          )
          .then((res) => {
            console.log(res);
            this.$emit("hideDialog");
            this.$emit("loadData");
          })
          .catch((res) => {
            console.log(res);
          });
      }
    },
  },
  props: {
    isShow: { type: Boolean, default: false },
    customer: { type: Object, default: null },
    formMode: { type: String, default: "add" },
  },
  created() {},
};
</script>
