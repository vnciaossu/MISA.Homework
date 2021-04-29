<template>
  <div>
    <div class="page-title">
      <div class="page-left">Danh sách khách hàng</div>
      <div class="page-right">
        <button id="btnAdd" class="btn-default" @click="btnAddOnClick()">
          Thêm khách hàng
        </button>
      </div>
    </div>
    <div class="toolbar">
      <input
        v-model="filters"
        @input="filterCustomer()"
        type="text"
        class="input-search"
        style="width: 220px;"
        placeholder="Tìm kiếm theo mã, tên khách hàng"
      />
      <button class="btn-refresh" @click="loadData()"></button>
      <button class="btn-delete" @click="btnDeleteOnClick(customerId)"></button>
    </div>
    <div class="grid">
      <table id="tblListCustomer" class="table" width="100%" border="0">
        <thead>
          <tr>
            <th>Mã khách hàng</th>
            <th>Họ và tên</th>
            <th>Giới tính</th>
            <th>Ngày sinh</th>
            <th>Nhóm khách hàng</th>
            <th>Điện thoại</th>
            <th>Email</th>
          </tr>
        </thead>
        <tbody>
          <tr
            v-for="customer in customers"
            :key="customer.customerId"
            @dblclick="trOnDblClick(customer.customerId)"
            @click="getCustomerId(customer.customerId)"
            v-bind:class="{
              customerClick: customer.customerId === customerClick,
            }"
          >
            <td>{{ customer.customerCode }}</td>
            <td>{{ customer.fullName }}</td>
            <td>{{ customer.genderName }}</td>
            <td>{{ fomatBirthday(customer.dateOfBirth) }}</td>
            <td>{{ customer.customerGroupName }}</td>
            <td>{{ customer.phoneNumber }}</td>
            <td>{{ customer.email }}</td>
          </tr>
        </tbody>
      </table>
    </div>
    <div class="paging">
      <div data-v-a72348a4="" class="paging-bar">
        <div data-v-a72348a4="" class="paging-record-info">
          Hiển thị <b data-v-a72348a4="">1-10/1000</b> nhân viên
        </div>
        <div data-v-a72348a4="" class="paging-option">
          <div
            v-if="page > 1"
            data-v-a72348a4=""
            class="btn-select-page m-btn-firstpage"
            @click="filterCustomer(1)"
          ></div>
          <div
            v-if="page > 1"
            data-v-a72348a4=""
            class="btn-select-page m-btn-prev-page"
            @click="filterCustomer((page = page - 1))"
          ></div>
          <div data-v-a72348a4="" class="m-btn-list-page">
            <button
              data-v-a72348a4=""
              class="btn-pagenumber"
              v-for="p in pages"
              :key="p"
              @click="filterCustomer(p)"
              :class="{ 'btn-pagenumber-selected': p == page }"
            >
              {{ p }}
            </button>
          </div>
          <div
            v-if="page < totalPages"
            data-v-a72348a4=""
            class="btn-select-page m-btn-next-page"
            @click="filterCustomer((page = page + 1))"
          ></div>
          <div
            v-if="page < totalPages"
            data-v-a72348a4=""
            class="btn-select-page m-btn-lastpage"
            @click="loadLastPage()"
          ></div>
        </div>
        <div data-v-a72348a4="" class="paging-record-option">
          <b data-v-a72348a4="">10</b> nhân viên/trang
        </div>
      </div>
    </div>
    <CustomerDetail
      :isShow="isShowDialogDetail"
      @hideDialog="hideDialog"
      :customer="customerDetail"
      :formMode="dialogFormMode"
      @loadData="loadData"
    />
  </div>
</template>

<script>
import CustomerDetail from "./CustomerDetail.vue";
import axios from "../../helpers/axios";
import dayjs from "dayjs";
export default {
  components: {
    CustomerDetail,
  },
  created() {
    this.loadData();
  },
  props: [],
  methods: {
    loadData() {
      axios
        .get("https://localhost:44321/api/v1/Customers/Filter?pageSize=20")
        .then((res) => {
          this.customers = res.data.data;
          this.totalPages = res.data.totalPages;
        })
        .catch((res) => {
          console.log(res);
        });
    },
    btnAddOnClick() {
      this.isShowDialogDetail = true;
      this.dialogFormMode = "add";
      this.customerDetail = {};
    },
    hideDialog() {
      this.isShowDialogDetail = false;
    },
    trOnDblClick(customerId) {
      axios
        .get("https://localhost:44321/api/v1/Customers/" + customerId)
        .then((res) => {
          this.customerDetail = res.data;
          console.log(res.data);
        })
        .catch((res) => {
          console.log(res);
        });

      //Cap nhap trang thai
      this.dialogFormMode = "edit";
      //Hien thi Dialog
      this.isShowDialogDetail = true;
    },
    fomatBirthday(cusbirthday) {
      if (cusbirthday == null) {
        return "Không có";
      }
      return dayjs(cusbirthday).format("DD/MM/YYYY");
    },
    getCustomerId(customerId) {
      this.customerId = customerId;
      this.customerClick = customerId;
    },
    btnDeleteOnClick(customerId) {
      axios
        .delete("https://localhost:44321/api/v1/Customers/" + customerId)
        .then((res) => {
          console.log(res);
          this.loadData();
        })
        .catch((res) => {
          console.log(res);
        });
    },
    filterCustomer(pageIndex) {
      clearTimeout(this.timeOut);
      this.timeOut = setTimeout(() => {
        axios
          .get(
            `https://localhost:44321/api/v1/Customers/Filter?pageSize=20&filter=${this.filters}&page=${pageIndex}`
          )
          .then((res) => {
            this.customers = res.data.data;
            this.page = pageIndex;
            console.log(res.data.data.length);
            if (res.data.data.length >= 20) {
              this.totalPages = pageIndex + 1;
            } else {
              this.totalPages = pageIndex;
            }
          })
          .catch((res) => {
            console.log(res);
          });
      }, 500);
    },
    loadLastPage() {
      axios
        .get(`https://localhost:44321/api/v1/Customers`)
        .then((res) => {
          pageIndex;
          var pageIndex = Math.round(res.data.length / 20) + 1;
          this.filterCustomer(pageIndex);
        })
        .catch((res) => {
          console.log(res);
        });
    },
  },
  data() {
    return {
      dialogFormMode: "add",
      isShowDialogDetail: false,
      customers: [],
      customerDetail: {},
      customerId: null,
      customerClick: null,
      dateFormat: "",
      timeOut: null,
      page: 1,
      totalPages: 0,
      filters: "",
    };
  },
  computed: {
    pages: function() {
      var pageArray = [];
      var start = this.page >= 3 ? this.page - 1 : 1;
      var end =
        this.page < this.totalPage - 2 ? this.page + 1 : this.totalPages;
      for (let i = start; i <= end; i++) {
        pageArray.push(i);
      }
      return pageArray;
    },
  },
  watch: {},
};
</script>

<style scoped>
@import "../../style/main.css";
@import "../../style/page/customer.css";

.customerClick {
  background-color: #dedede;
}

.paging-option {
  display: flex;
}

.btn-pagenumber {
  background-position: center;
}

.m-btn-firstpage {
  height: 30px;
  width: 30px;
  background-image: url("../../assets/icon/btn-firstpage.svg");
  background-position: center;
  background-size: inherit;
  background-repeat: no-repeat;
}

.m-btn-prev-page {
  height: 30px;
  width: 30px;
  background-image: url("../../assets/icon/btn-prev-page.svg");
  background-position: center;
  background-size: inherit;
  background-repeat: no-repeat;
}

.m-btn-next-page {
  height: 30px;
  width: 30px;
  background-image: url("../../assets/icon/btn-next-page.svg");
  background-position: center;
  background-size: inherit;
  background-repeat: no-repeat;
}

.m-btn-lastpage {
  height: 30px;
  width: 30px;
  background-image: url("../../assets/icon/btn-lastpage.svg");
  background-position: center;
  background-size: inherit;
  background-repeat: no-repeat;
}

.btn-select-page {
  cursor: pointer;
}
</style>
