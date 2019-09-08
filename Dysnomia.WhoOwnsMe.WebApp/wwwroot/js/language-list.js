var iconSelect;
var selectedText;

window.addEventListener("load", function(){
    
    // Variables de base
    selectedText = document.getElementById('selected-language');
    languageForm = document.getElementById('language-form');
    changed = 0;
    
    // Evenement du changement de drapeau
    document.getElementById('language-list').addEventListener('changed', function(e){
       selectedText.value = iconSelect.getSelectedValue();
       changed++;
       if(changed==2) { // Car sinon il s'envoie dès le chargement de la page
           languageForm.submit();
       }
    });
    
    // Chargement de la liste des drapeaux
    iconSelect = new IconSelect("language-list");

    var icons = [];
    icons.push({'iconFilePath':'https://03.cdn.elanis.eu/website/img/flags/fr.png', 'iconValue':'Francais'});
    icons.push({'iconFilePath':'https://03.cdn.elanis.eu/website/img/flags/en.png', 'iconValue':'English'});
    //icons.push({'iconFilePath':'https://03.cdn.elanis.eu/website/img/flags/ru.png', 'iconValue':'русский'});
    //icons.push({'iconFilePath':'https://03.cdn.elanis.eu/website/img/flags/de.png', 'iconValue':'Deutsch'});
    
    iconSelect.refresh(icons);
});