function createRow_text(sbj_Name, hr, min, sbj__) { // parameters: from, text
    //alert(sbj_Name + ': ' + hr + '/' + min + '/' + sbj__);

    //alert(sbj__);
    //alert("0");
    var id = "div-" + sbj__;
    //alert(id);
    var table = document.getElementById("div-" + sbj__);
    //alert("1");
    var div_4 = document.createElement("DIV");
    div_4.className = ("d-inline-flex p-0 pl-2");
    //div_4.addClass("p-0");
    //div_4.addClass("pb-1");
    //alert("2");
    var span_1 = document.createElement("SPAN");
    span_1.className = "p-0";
    span_1.innerHTML = "- กลุ่มสาระการเรียนรู้/รายวิชา ";
    //alert("3");
    var inp_1 = document.createElement("SPAN");
    inp_1.className = sbj__ + "-Name pt-0 pl-2 form_input";
    inp_1.innerHTML = sbj_Name;

    var span_2 = document.createElement("SPAN");
    span_2.className = "pt-0 pl-2";
    span_2.innerHTML = " จำนวน ";
    var inp_2 = document.createElement("SPAN");
    inp_2.className = sbj__ + "-hour pt-0 pl-2 form_input";
    inp_2.innerHTML = hr;

    var span_3 = document.createElement("SPAN");
    span_3.className = "pt-0 pl-2";
    span_3.innerHTML = " ชั่วโมง ";
    var inp_3 = document.createElement("SPAN");
    inp_3.className = sbj__ + "-min pt-0 pl-2 form_input";
    inp_3.innerHTML = min;
    //alert("4");
    var span_4 = document.createElement("SPAN");
    span_4.className = "pt-0 pl-2";
    span_4.innerHTML = "นาที/สัปดาห์";
    //<input type="text" id="subject" name="subject" class="form-control form-control-lg ml-3 mr-3" style="font-size: 14px; height: 35px;" />
    div_4.appendChild(span_1);
    div_4.appendChild(inp_1);
    div_4.appendChild(span_2);
    div_4.appendChild(inp_2);
    div_4.appendChild(span_3);
    div_4.appendChild(inp_3);
    div_4.appendChild(span_4);
    //alert(div_4);
    //table.insertBefore(div_4, table.lastElementChild);
    table.appendChild(div_4);
    //alert("5");
}

function calculateTotal_text(sbj_, total) {
    var sum_tot = total;
    //alert('cal');
    var id_ = "Total_Hour_" + sbj_;
    //alert(id_);
    var total_H = document.getElementById("Total_Hour_" + sbj_);
    total_H.innerHTML = '0';
    var total_M = document.getElementById("Total_Min_" + sbj_);
    total_M.innerHTML = '0';
    //alert(total_);
    var min_remainder = Number(sum_tot) % 60;
    //alert(min_remainder);
    var hour_mod = (Number(sum_tot) - Number(min_remainder)) / 60;
    //alert(hour_mod);
    total_H.innerHTML = hour_mod;
    total_M.innerHTML = min_remainder;
}

function createRow(sbj_Name, hr, min, sbj) {

    //var targetTabel = document.getElementById(table-subject);
    //<div id="mdiv" style="padding:5px 0px 4px 6px;" onclick="deleteItem();alert('Delete subject');">
    //    <div class="mdiv">
    //        <div class="md" ></div>
    //    </div>
    //</div>
    //alert(sbj);
    var UUID_ = uuidv4();
    //alert('Add subject');
    var table_ = "table-" + sbj;
    var table = document.getElementById(table_);
    var lastRow = table.rows[table.rows.length - 1];
    var lastRowIndex = table.rows.length - 1;
    //alert(table.rows[lastRowIndex]);
    var div_1 = document.createElement("DIV");

    div_1.id = "mdiv";
    div_1.className = uuidv4();
    div_1.style.padding = "5px 0px 4px 6px";
    div_1.setAttribute("onclick", "deleteItem(this);");
    var div_2 = document.createElement("DIV");
    div_2.className = "mdiv";
    var div_3 = document.createElement("DIV");
    div_3.className = "md";
    div_2.appendChild(div_3)
    div_1.appendChild(div_2)


    var divTagTd1 = document.createElement("td");

    divTagTd1.style.padding = "5px 0px 5px 5px";
    var divTagTd2 = document.createElement("td");
    divTagTd2.style.padding = "0px";
    divTagTd2.style.fontSize = "14px";
    //divTagTd2.innerHTML = "";

    var div_4 = document.createElement("DIV");
    div_4.className = ("d-inline-flex pt-1 pb-1");
    //div_4.addClass("pt-1");
    //div_4.addClass("pb-1");
    var span_1 = document.createElement("SPAN");
    span_1.className = "pt-1";
    span_1.innerHTML = "กลุ่มสาระการเรียนรู้/รายวิชา";
    var span_2 = document.createElement("SPAN");
    span_2.className = "pt-1";
    span_2.innerHTML = "จำนวน";
    var span_3 = document.createElement("SPAN");
    span_3.className = "pt-1";
    span_3.innerHTML = "ชั่วโมง";

    var span_4 = document.createElement("SPAN");
    span_4.className = "pt-1";
    span_4.innerHTML = "นาที /สัปดาห์";


    var inp_1 = document.createElement("INPUT");
    inp_1.className = "form-control form-control-lg ml-3 mr-3";
    inp_1.id = sbj; //"subject";
    inp_1.style.fontSize = "14px";
    inp_1.style.width = "300px";
    inp_1.style.height = "35px";
    inp_1.setAttribute("type", "text");
    inp_1.setAttribute("name", sbj + "-name");
    inp_1.value = sbj_Name;

    var inp_2 = document.createElement("INPUT");
    inp_2.className = "form-control form-control-lg ml-3 mr-3 p-0 text-center";
    inp_2.id = sbj + "-hour";
    //inp_2.name = "subject-hour";
    inp_2.style.fontSize = "14px";
    inp_2.style.width = "50px";
    inp_2.style.height = "35px";
    inp_2.setAttribute("type", "text");
    inp_2.setAttribute("name", sbj + "-hour");
    inp_2.value = hr;

    var inp_3 = document.createElement("INPUT");
    inp_3.className = "form-control form-control-lg ml-3 mr-3 p-0 text-center";
    inp_3.id = sbj + "-min";
    //inp_2.name = "subject-hour";
    inp_3.style.fontSize = "14px";
    inp_3.style.width = "50px";
    inp_3.style.height = "35px";
    inp_3.setAttribute("type", "text");
    inp_3.setAttribute("name", sbj + "-min");
    inp_3.value = min;


    inp_1.addEventListener("input", function (e) {
        //alert(this.id);
        var a, b, i, val = this.value;

    });
    inp_1.addEventListener("keydown", function (e) {

        if (e.keyCode == 40) {
            /*If the arrow DOWN key is pressed,
            increase the currentFocus variable:*/
            //currentFocus++;
            /*and and make the current item more visible:*/
            //addActive(x);
        } else if (e.keyCode == 38) { //up
            /*If the arrow UP key is pressed,
            decrease the currentFocus variable:*/
            //currentFocus--;
            /*and and make the current item more visible:*/
            //addActive(x);
        } else if (e.keyCode == 13) {
            /*If the ENTER key is pressed, prevent the form from being submitted,*/
            e.preventDefault();
            //if (currentFocus > -1) {
            //    /*and simulate a click on the "active" item:*/
            //    if (x) x[currentFocus].click();
            //}
        }
    });

    inp_2.addEventListener("keyup", function (e) {
        //alert(this.nodeValue);
        //alert(sbj);
        calculateTotal(sbj);
    });
    inp_2.addEventListener("keydown", function (e) {
        if (e.keyCode == 40) {
            /*If the arrow DOWN key is pressed,
            increase the currentFocus variable:*/
            //currentFocus++;
            /*and and make the current item more visible:*/
            //addActive(x);
        } else if (e.keyCode == 38) { //up
            /*If the arrow UP key is pressed,
            decrease the currentFocus variable:*/
            //currentFocus--;
            /*and and make the current item more visible:*/
            //addActive(x);
        } else if (e.keyCode == 13) {
            /*If the ENTER key is pressed, prevent the form from being submitted,*/
            e.preventDefault();
            //if (currentFocus > -1) {
            //    /*and simulate a click on the "active" item:*/
            //    if (x) x[currentFocus].click();
            //}
        }

    });

    inp_3.addEventListener("keyup", function (e) {
        //alert(this.nodeValue);
        //alert(sbj);
        calculateTotal(sbj);
    });
    inp_3.addEventListener("keydown", function (e) {
        if (e.keyCode == 40) {
            /*If the arrow DOWN key is pressed,
            increase the currentFocus variable:*/
            //currentFocus++;
            /*and and make the current item more visible:*/
            //addActive(x);
        } else if (e.keyCode == 38) { //up
            /*If the arrow UP key is pressed,
            decrease the currentFocus variable:*/
            //currentFocus--;
            /*and and make the current item more visible:*/
            //addActive(x);
        } else if (e.keyCode == 13) {
            /*If the ENTER key is pressed, prevent the form from being submitted,*/
            e.preventDefault();
            //if (currentFocus > -1) {
            //    /*and simulate a click on the "active" item:*/
            //    if (x) x[currentFocus].click();
            //}
        }

    });


    //<input type="text" id="subject" name="subject" class="form-control form-control-lg ml-3 mr-3" style="font-size: 14px; height: 35px;" />

    div_4.appendChild(span_1);
    div_4.appendChild(inp_1);
    div_4.appendChild(span_2);
    div_4.appendChild(inp_2);
    div_4.appendChild(span_3);
    div_4.appendChild(inp_3);
    div_4.appendChild(span_4);

    divTagTd2.appendChild(div_4);
    var divTr = document.createElement("tr");
    divTr.id = UUID_;
    divTr.style.outerHeight = "44px";
    divTagTd1.appendChild(div_1);
    divTr.appendChild(divTagTd1);
    divTr.appendChild(divTagTd2);

    //document.getElementById("tbody-subject").appendChild(divTr);

    table.insertBefore(divTr, table.lastElementChild);

}

function calculateTotal(sbj_) {

    let total = document.getElementById("Total_Hour_" + sbj_);
    total.value = '0' + '   ชั่วโมง   ' + '0' + '   นาที';
    //alert(total);
    //total.value = e.target.value;
    //alert(inputTextValue);
    let input = document.getElementsByName(sbj_ + "-hour");
    let input_min = document.getElementsByName(sbj_ + "-min");
    var sum_ = 0;
    var sum_min = 0;
    var sum_tot = 0;
    for (var i = 0; i < input.length; i++) {

        if (isNaN(input[i].value)) { }
        else {
            sum_ = Number(sum_) + Number(input[i].value);

            sum_min = Number(sum_min) + Number(input_min[i].value);
            //sum_ = Number(sum_) + Number(this_input_[i].value);
            sum_tot = Number(sum_) * 60 + Number(sum_min);

        }

    }

    var min_remainder = Number(sum_tot) % 60;
    //alert(min_remainder);
    var hour_mod = (Number(sum_tot) - Number(min_remainder)) / 60;
    //alert(hour_mod);
    total.value = hour_mod + '   ชั่วโมง   ' + min_remainder + '   นาที';


}
