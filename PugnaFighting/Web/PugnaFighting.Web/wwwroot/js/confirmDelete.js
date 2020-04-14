let sellSpan = document.getElementById('delete-span');
let deleteQuestion = document.getElementById('delete-question');
let deleteButton = document.getElementById('delete-btn');
let cancelButton = document.getElementById('cancel-btn');

sellSpan.addEventListener('click', () => {
    sellSpan.style.display = 'none';
    deleteQuestion.style.display = 'block';
    deleteButton.style.display = 'block';
    cancelButton.style.display = 'block';
});

cancelButton.addEventListener('click', () => {
    sellSpan.style.display = 'block';
    deleteQuestion.style.display = 'none';
    deleteButton.style.display = 'none';
    cancelButton.style.display = 'none';
});