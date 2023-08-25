// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", function () {
    
    var alterarLinks = document.querySelectorAll(".alterar-link");

    alterarLinks.forEach(function (link) {
        link.addEventListener("click", function () {
            
            var codigo = this.getAttribute("data-codigo");
            var nome = this.getAttribute("data-nome");
            var custo = this.getAttribute("data-custo");
            var data = this.getAttribute("data-data");

            document.getElementById("codigoTarefa").value = codigo;
            document.getElementById("nomeTarefa").value = nome;
            document.getElementById("valorcusto").value = custo;
            document.getElementById("dtLimite").value = data;

            
            document.getElementById("popup").style.display = "block";
        });
    });
});


document.addEventListener("DOMContentLoaded", function () {
    const excluirLinks = document.querySelectorAll(".excluir-link");
    excluirLinks.forEach(link => {
        link.addEventListener("click", function (event) {
            event.preventDefault();

            const codigoTarefa = this.getAttribute("data-codigoTarefa");

            if (window.confirm('Deseja realmente excluir a tarefa?')) {
                excluirTarefa(codigoTarefa);
            }
        });
    });
});

function excluirTarefa(codigoTarefa) {
    fetch(`/Home/ExcluirTarefa?codigoTarefa=${codigoTarefa}`, {
        method: 'POST' 
    })
        .then(response => response.json())
        .then(data => {
            alert(data.mensagem); 
            location.reload();
        })
        .catch(error => {
            console.error('Erro ao excluir tarefa:', error);
            location.reload();
        });
}

    document.addEventListener('DOMContentLoaded', function () {
        var dragSrcEl = null;

    function handleDragStart(e) {
        dragSrcEl = this;
    e.dataTransfer.effectAllowed = 'move';
    e.dataTransfer.setData('text/html', this.outerHTML);
    this.classList.add('dragged');
        }

    function handleDragOver(e) {
            if (e.preventDefault) {
        e.preventDefault();
            }
    this.classList.add('over');
    e.dataTransfer.dropEffect = 'move';
    return false;
        }

    function handleDragEnter(e) {
            if (this !== dragSrcEl) {
        this.classList.add('over');
            }
        }

    function handleDragLeave() {
        this.classList.remove('over');
        }

    function handleDrop(e) {
            if (e.stopPropagation) {
        e.stopPropagation();
            }
    if (dragSrcEl !== this) {
        dragSrcEl.outerHTML = this.outerHTML;
    this.outerHTML = e.dataTransfer.getData('text/html');
            }
    return false;
        }

    function handleDragEnd() {
        this.classList.remove('over');
    this.classList.remove('dragged');
        }

    var cols = document.querySelectorAll('tr.draggable');
    [].forEach.call(cols, function (col) {
        col.addEventListener('dragstart', handleDragStart, false);
    col.addEventListener('dragenter', handleDragEnter, false);
    col.addEventListener('dragover', handleDragOver, false);
    col.addEventListener('dragleave', handleDragLeave, false);
    col.addEventListener('drop', handleDrop, false);
    col.addEventListener('dragend', handleDragEnd, false);
        });
    });


