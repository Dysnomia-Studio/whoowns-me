window.addEventListener('load',() => {

	// Variables de base
	const selectedText = document.getElementById('selected-language');
	const languageForm = document.getElementById('language-form');
	let   changed = 0;
	// Chargement de la liste des drapeaux
	const iconSelect = new IconSelect('language-list');

	// Evenement du changement de drapeau
	document.getElementById('language-list').addEventListener('changed',(e) => {
		selectedText.value = iconSelect.getSelectedValue();
		changed++;
		if(changed === 2) { // Car sinon il s'envoie dès le chargement de la page
			languageForm.submit();
		}
	});

	const icons = [];
	icons.push({'iconFilePath':'https://03.cdn.elanis.eu/website/img/flags/fr.png', 'iconValue':'Francais'});
	icons.push({'iconFilePath':'https://03.cdn.elanis.eu/website/img/flags/en.png', 'iconValue':'English'});
	//icons.push({'iconFilePath':'https://03.cdn.elanis.eu/website/img/flags/ru.png', 'iconValue':'русский'});
	//icons.push({'iconFilePath':'https://03.cdn.elanis.eu/website/img/flags/de.png', 'iconValue':'Deutsch'});

	iconSelect.refresh(icons);
});
