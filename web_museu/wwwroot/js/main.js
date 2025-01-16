// Menu Responsivo
var menuIcon = document.querySelector('.menu-icon');
var ul = document.querySelector('.ul');

if (menuIcon && ul) {
    menuIcon.addEventListener('click', () => {
        if (ul.classList.contains('ativo')) {
            ul.classList.remove('ativo');
            document.querySelector('.menu-icon img').src = 'img/menu.png';
        } else {
            ul.classList.add('ativo');
            document.querySelector('.menu-icon img').src = 'img/close.png';
        }
    });
}

// Slider Automático e Manual
currentIndex = 0; // Sem 'let' ou 'const' aqui
const slides = document.querySelectorAll('.slides .slide'); // Seleciona todos os slides
const slider = document.querySelector('.slides'); // Container dos slides
const totalSlides = slides.length; // Total de slides
const intervalTime = 5000; // Intervalo de 5 segundos

// Função para mover o slider
function moveSlider(index) {
    currentIndex = index;
    slider.style.transform = `translateX(-${index * 100}%)`; // Move o slider
    updateButtons(); // Atualiza os botões manuais
}

// Atualiza os botões manuais
function updateButtons() {
    const buttons = document.querySelectorAll('.manual-btn');
    buttons.forEach((btn, index) => {
        btn.style.backgroundColor = index === currentIndex ? '#FFF' : '#000'; // Botão ativo em branco
    });
}

// Alterna automaticamente os slides
setInterval(() => {
    currentIndex = (currentIndex + 1) % totalSlides; // Próximo slide
    moveSlider(currentIndex);
}, intervalTime);

// Configuração dos botões manuais
document.querySelectorAll('.manual-btn').forEach((btn, index) => {
    btn.addEventListener('click', () => {
        moveSlider(index); // Move para o slide correspondente
    });
});

// DataTables
$(document).ready(function () {
    initializeDatatables('#table-contatos');
    initializeDatatables('#table-usuarios');
});

function initializeDatatables(id) {
    $(id).DataTable({
        "ordering": true,
        "paging": true,
        "searching": true,
        "oLanguage": {
            "sEmptyTable": "Nenhum registro encontrado na tabela",
            "sInfo": "Mostrar _START_ até _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrar 0 até 0 de 0 Registros",
            "sInfoFiltered": "(Filtrar de _MAX_ total registros)",
            "sInfoThousands": ".",
            "sLengthMenu": "Mostrar _MENU_ registros por página",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Pesquisar",
            "oPaginate": {
                "sNext": "Próximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "Último"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        }
    });
}

// Ocultar Alerta
$(".close-alert").click(function () {
    $(".alert").fadeOut("slow");
});
