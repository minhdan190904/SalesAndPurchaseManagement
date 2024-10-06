var ip_name = document.querySelector('#name');
var ip_pass = document.querySelector('#password');
function check_null(text) {
    if (text.value.trim() == '') return true;
    else return false;
}
function check() {
    if (check_null(ip_name) || check_null(ip_pass)) return false;
    else return true;
}
var btn = document.querySelector('.login');
var Warning = document.querySelector(".warning");
Object.prototype.enable = function () {
    this.removeAttribute('disabled');
    this.style.backgroundColor = "gray";
    this.style.color = 'white';
}
Object.prototype.disable = function () {
    this.setAttribute('disabled', 'disabled');
    this.style.backgroundColor = "whitesmoke";
    this.style.color = 'black';
    Warning.innerText = "";
}
ip_name.oninput = function () {
    if (!check()) {
        btn.disable();
    }
    else {
        btn.enable();
    }
}
ip_pass.oninput = function () {
    if (!check()) {
        btn.disable();
    }
    else {
        btn.enable();
    }
}