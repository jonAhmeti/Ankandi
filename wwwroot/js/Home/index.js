const body = document.querySelector("body");
const modal = document.querySelector(".modal");
const modalButton = document.querySelector(".modal-button");
const closeButton = document.querySelector(".close-button");
const scrollDown = document.querySelector(".scroll-down");
let isOpened = false;

const openModal = () => {
    modal.classList.add("is-open");
    body.style.overflow = "hidden";
};

const closeModal = () => {
    modal.classList.remove("is-open");
    body.style.overflow = "initial";
};

window.addEventListener("scroll", () => {
    if (window.scrollY > window.innerHeight / 3 && !isOpened) {
        isOpened = true;
        scrollDown.style.display = "none";
        openModal();
    }
});

modalButton.addEventListener("click", openModal);
closeButton.addEventListener("click", closeModal);

document.onkeydown = evt => {
    evt = evt || window.event;
    evt.keyCode === 27 ? closeModal() : false;
};

$(function() {

    //get current culture/lang
    const culture = $("#culture").children("option:selected").val();

    const signUpBtn = $("#signUpBtn");
    const formWrap = $("#formWrapper");
    signUpBtn.on("click",
        function () {
            formWrap.html("");
            culture === "sq"
                ? formWrap.append(`
                <form action="/SignUp" method="post">
                    <div class="input-block">
                        <label for="email" class="input-label">Përdoruesi</label>
                        <input type="text" name="username" id="email" placeholder="Përdoruesi">
                    </div>
                    <div class="input-block">
                        <label for="password" class="input-label">Fjalëkalimi</label>
                        <input type="password" name="password" id="password" placeholder="Fjalëkalimi">
                    </div>
                    <div class="input-block">
                        <label for="name" class="input-label">Emri i plotë</label>
                        <input type="text" name="name" id="name" placeholder="Emri dhe mbiemri">
                    </div>
                    <div class="input-block">
                        <label for="dob" class="input-label">Data e lindjes</label>
                        <input type="date" name="dob" id="dob" value="1999-01-01">
                    </div>
                    <div class="modal-buttons">
                        <button class="input-button">Regjistrohu</button>
                    </div>
                    <p>Keni një llogari? <a id="loginBtn" style="cursor: pointer">Kyçu</a></p>
                </form>
                `)
                : formWrap.append(`
                <form action="/SignUp" method="post">
                    <div class="input-block">
                        <label for="email" class="input-label">Username</label>
                        <input type="text" name="username" id="email" placeholder="Username">
                    </div>
                    <div class="input-block">
                        <label for="password" class="input-label">Password</label>
                        <input type="password" name="password" id="password" placeholder="Password">
                    </div>
                    <div class="input-block">
                        <label for="name" class="input-label">Full name</label>
                        <input type="text" name="name" id="name" placeholder="Your full name">
                    </div>
                    <div class="input-block">
                        <label for="dob" class="input-label">Date of birth</label>
                        <input type="date" name="dob" id="dob" value="1999-01-01">
                    </div>
                    <div class="modal-buttons">
                        <button class="input-button">Signup</button>
                    </div>
                    <p>Already have an account? <a id="loginBtn" style="cursor: pointer">Login</a></p>
                </form>
                `);
        });
});