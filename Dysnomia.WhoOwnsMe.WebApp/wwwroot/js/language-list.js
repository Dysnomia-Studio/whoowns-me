let changed = 0;
const iconSelect = new IconSelect('language-list');

window.addEventListener('load', () => {

	// Variables de base
	const selectedText = document.getElementById('selected-language');

	// Chargement de la liste des drapeaux
	const icons = [];
	icons.push({ 'iconFilePath': 'https://03.cdn.elanis.eu/website/img/flags/fr.png', 'iconValue': 'fr' });
	icons.push({ 'iconFilePath': 'https://03.cdn.elanis.eu/website/img/flags/en.png', 'iconValue': 'en' });
	//icons.push({'iconFilePath':'https://03.cdn.elanis.eu/website/img/flags/ru.png', 'iconValue':'ru'});
	//icons.push({'iconFilePath':'https://03.cdn.elanis.eu/website/img/flags/de.png', 'iconValue':'de'});

	// Evenement du changement de drapeau
	document.getElementById('language-list').addEventListener('changed', (e) => {
		selectedText.value = iconSelect.getSelectedValue();
		changed++;
		if (changed === 2) { // Car sinon il s'envoie dès le chargement de la page
			let oldHref = window.location.href.replace(window.location.origin, '');

			// Remove language from URL
			for (const icon of icons) {
				oldHref = oldHref.replace('/' + icon.iconValue + '/', '/');
			}

			window.location.href = window.location.origin + '/' + selectedText.value + oldHref;
		}
	});

	iconSelect.refresh(icons);
});
