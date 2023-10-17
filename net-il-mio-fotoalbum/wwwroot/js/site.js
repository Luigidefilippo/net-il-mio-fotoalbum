// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/*----------------------------------------------------- */
function getFoto() {
    axios.get("/api/Fotos/getFoto?filter=" + document.getElementById("cerca").value)
        .then(resp => {
            document.getElementById('card-container').innerHTML = "";
            resp.data.forEach((foto) => {
                document.getElementById('card-container').innerHTML +=
                    `
                    <div class="card p-2" style ="width:40%; min-width:200px">
                            <img class="card-image w-100" style= "height: 180px; object-fit:contain;" src="${foto.imagesrcFile}">
                            <div class="card-header">${foto.title}</div>
                            <div class="card-header">${foto.description}</div>
                            <div class="card-header">${foto.categories.map(category => category.name)}</div>
                            <div class="card-body overflow-hidden px-1 pb-4">
                                <a class="btn btn-primary" href="/Foto/Dettagli/${foto.id}" 
                                    <i class="fa-solid fa-magnifying-glass">Dettagli</i> 
                                </a>
                        </div>
                    `
            });
        })
        .catch((error) => alert(error))
}

function formSubmit(e) {
    e.preventDefault();
    const userEmail = document.getElementById("email").value
    const userMessage = document.getElementById("textMessage").value.trim();
    const done = document.getElementById("done")

    console.log(userEmail, userMessage)
}

axios.post("api/usermessage", { Title: userEmail, Text: userMessage })
    .then(resp => {
        done.classList.remove("d-none")
    })
    .catch(error => {
        console.log(error)

        done.classList.remove("d-none")

        done.classList.add("text-error")
    })
}



document.getElementById('send-button').addEventListener('click', function (event) {
    event.preventDefault();

    
    alert("Messaggio inviato");
});