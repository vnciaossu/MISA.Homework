$(document).ready(function () {
  
  addEvent();
});

const genderList = [
  { value: 0, text: "Nữ" },
  { value: 1, text: "Nam" },
  { value: 2, text: "Khác" },
];

function addEvent() {
  $("#btn_down").on("click", function () {
    if ($("li").html()) {
      $("#ul_list").html("");
      $("#ul_list").addClass("hide");
    } else {
      showGender(genderList);
    }
  });

  $("#inp_text").on("hover", function () {
    showGender(genderList);
  });

  $(document).on("click", "#ul_list li", function () {
    var e = $(this);
    clickGender(e);
    if ($("li").html()) {
      $("#ul_list").html("");
      $("#ul_list").addClass("hide");
    }
  });

  $("#inp_text").on("input", function () {
    let val = $(this).val().trim();
    let dataList = genderList.filter((item) =>
      item.text.toLocaleLowerCase().includes(val.toLocaleLowerCase())
    );
    showGender(dataList, val);
  });

  $(".cb-autocomplete").on("keydown",function(e){
    buttonEvent(e);
  })
}

function showGender(dataList, text) {
  if (text != "" && dataList == "") {
    $("#inp_text").css("border-color", "red");
    $("#btn_down ").css("border-color", "red");
  } else {
    $("#inp_text").css("border-color", "green");
    $("#btn_down ").css("border-color", "green");
  }
  $("#ul_list").html("");
  dataList.forEach((item) => {
    var li_html = $(`
    <li>
        <span class="item-icon"></span>
        <span class="item-text">${item.text}</span>
    </li>
  `);
    if (text == item.text) {
      li_html.addClass("selected");
    }
    li_html.data("gender", item);
    $("#ul_list").append(li_html);
  });
  $("#ul_list").removeClass("hide");
}

function clickGender(e) {
  $(e).siblings(".selected").removeClass("selected");
  $(e).addClass("selected");
  var genderCheck = $(e).data("gender");
  $("#inp_text").val(genderCheck.text);
}

var index = -1;
function buttonEvent(e){
  let keyCode = e.keyCode;
  let count = $("#ul_list").find("li").length;
  
  if(keyCode == 40){
    if(index < count - 1){
      $("#ul_list").find("li").eq(index).removeClass("hover");
      let _this = $("#ul_list").find("li").eq(index + 1).addClass("hover");
      index += 1;
      clickGender(_this);
    }
  }else if(keyCode == 38){
    if(index > 0){
      $("#ul_list").find("li").eq(index).removeClass("hover");
      let _this = $("#ul_list").find("li").eq(index-1).addClass("hover");
      index--;
      clickGender(_this);
    }
  }else if(keyCode == 13){
    let _this = $("#ul_list").find("li.hover");
    clickGender(_this);
  }

}