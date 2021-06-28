window.addEventListener('load', () => {
	var textareas = document.getElementsByTagName('textarea');

	Array.from(textareas).forEach(text => {
		function resize() {
			text.style.height = 'auto';
			text.style.height = text.scrollHeight + 'px';
		}
		/* 0-timeout to get the already changed text */
		function delayedResize() {
			window.setTimeout(resize, 0);
		}
		text.addEventListener('change', resize);
		text.addEventListener('cut', delayedResize);
		text.addEventListener('paste', delayedResize);
		text.addEventListener('drop', delayedResize);
		text.addEventListener('keydown', delayedResize);

		text.focus();
		text.select();
		resize();
	});
});