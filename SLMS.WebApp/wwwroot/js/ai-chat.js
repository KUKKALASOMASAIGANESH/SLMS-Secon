$(document).ready(function () {

    // Open Chat
    $("#aiButton").click(function () {

        $("#aiChatBox").fadeIn(200);

        $("#txtQuestion").focus();

    });

    // Close Chat
    $("#closeAI").click(function () {

        $("#aiChatBox").fadeOut(200);

    });

    // Send Button
    $("#btnSend").click(function () {

        sendQuestion();

    });

    // Enter Key
    $("#txtQuestion").on("keydown", function (e) {

        if (e.key === "Enter") {

            e.preventDefault();

            sendQuestion();

        }

    });

    // Quick Questions
    $(".quickQuestion").click(function () {

        $("#txtQuestion").val($(this).text().trim());

        sendQuestion();

    });

});

function sendQuestion() {

    let question = $("#txtQuestion").val().trim();

    if (question === "")
        return;

    // Show User Message
    addUserMessage(question);

    // Clear Input
    $("#txtQuestion").val("");

    // Keep Cursor
    $("#txtQuestion").focus();

    // Show Typing Animation
    showTyping();

    $.ajax({

        url: "/AI/Ask",

        type: "POST",

        data: {

            question: question

        },

        success: function (data) {

            hideTyping();

            addAIMessage(data.answer);

            $("#txtQuestion").focus();

        },

        error: function () {

            hideTyping();

            addAIMessage("⚠ Unable to contact AI Service.");

            $("#txtQuestion").focus();

        }

    });

}

function addUserMessage(text) {

    let time = new Date().toLocaleTimeString([], {

        hour: '2-digit',

        minute: '2-digit'

    });

    $("#chatMessages").append(`

        <div class="text-end mb-3">

            <div class="d-inline-block bg-primary text-white rounded p-3 shadow">

                ${text}

            </div>

            <br>

            <small class="text-muted">

                ${time}

            </small>

        </div>

    `);

    scrollChat();

}

function addAIMessage(text) {

    let id = "msg" + Date.now();

    let time = new Date().toLocaleTimeString([], {

        hour: '2-digit',

        minute: '2-digit'

    });

    $("#chatMessages").append(`

        <div class="mb-3">

            <div class="d-inline-block bg-light rounded p-3 shadow">

                <div class="fw-bold text-primary">

                    🤖 SECON AI

                </div>

                <hr>

                <div id="${id}"></div>

                <small class="text-muted d-block text-end mt-2">

                    ${time}

                </small>

            </div>

        </div>

    `);

    typeWriter(id, text);

    scrollChat();

}

function showTyping() {

    $("#chatMessages").append(`

        <div id="typing">

            <div class="d-inline-block bg-light rounded p-3 shadow mb-3">

                🤖 <strong>SECON AI</strong>

                <br><br>

                <div class="typing">

                    <span></span>

                    <span></span>

                    <span></span>

                </div>

            </div>

        </div>

    `);

    scrollChat();

}

function hideTyping() {

    $("#typing").remove();

}

function typeWriter(id, text) {

    let words = text.split(" ");

    let i = 0;

    let timer = setInterval(function () {

        $("#" + id).append(words[i] + " ");

        scrollChat();

        i++;

        if (i >= words.length) {

            clearInterval(timer);

        }

    }, 35);

}

function scrollChat() {

    $("#chatMessages").stop().animate({

        scrollTop: $("#chatMessages")[0].scrollHeight

    }, 200);

} 