mergeInto(LibraryManager.library, {
    
    SetUPRecognizer: function(words) {
        const recognizer = new webkitSpeechRecognition();
        const speechList = new webkitSpeechGrammarList();

        const commands = UTF8ToString(words).split(',');
        const grammar = '#JSGF V1.0; grammar commands; public <command> = ' + commands.join(' | ') + ' ;';

        speechList.addFromString(grammar, 1);
        recognizer.grammars = speechList;


        recognizer.addEventListener('result', (event) => {
            const result = event.results[event.resultIndex];
            if (result.isFinal) {
                const command = result[0].transcript;
                unityGame.SendMessage('SpeechRecognizer', 'OnFinalResult', command);
            }
        });

        
        recognizer.addEventListener('end', () => {
            recognizer.start();
        });

        recognizer.start();
    }
})