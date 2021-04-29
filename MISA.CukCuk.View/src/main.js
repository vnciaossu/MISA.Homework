import Vue from "vue";
import App from "./App.vue";
import VueRouter from "vue-router";
import "nprogress/nprogress.css";

//import components
import CustomerList from "./view/customer/CustomerList.vue";
import EmployeeList from "./view/employee/EmployeeList.vue";
import ReportView from "./view/report/ReportView.vue";
import Index from "./view/Index.vue";
import SettingView from "./view/setting/SettingView.vue";

const routes = [
  { path: "/index", component: Index },
  { path: "/customer", component: CustomerList },
  { path: "/employee", component: EmployeeList },
  { path: "/report", component: ReportView },
  { path: "/setting", component: SettingView },
];

const router = new VueRouter({
  routes,
});
Vue.config.productionTip = false;
//vue-router started
Vue.use(VueRouter);

new Vue({
  router,
  render: (h) => h(App),
}).$mount("#app");
