mergeInto(LibraryManager.library, {
    
    SetUPRecognizer: function(words) {
        const recognizer = new webkitSpeechRecognition();
        const speechList = new webkitSpeechGrammarList();

        const commands = UTF8ToString(words).toLowerCase().split(',');
        console.log("commands",commands);
        const grammar = '#JSGF V1.0; grammar commands; public <command> = ' + commands.join(' | ') + ' ;';

        speechList.addFromString(grammar, 1);
        recognizer.grammars = speechList;

        this.recognizer = recognizer;


        recognizer.addEventListener('result', (event) => {
            const result = event.results[0][0].transcript;
            const words = result.split(' ');
            console.log("words", words);

            for(let i = 0; i < words.length; i++) {
                const word = words[i].toLowerCase().replace(/[^a-z]/g, '');
                if(commands.includes(word)) {
                    console.log("word",word);
                    unityGame.SendMessage('SpeechRecognizer', 'OnFinalResult', word);
                    break;
                }                
            }
        });

        
        recognizer.addEventListener('end', () => {
            recognizer.start();
        });

        recognizer.start();
    },

    StartRecognizer: function() {
        this.recognizer.start();
    },

    StopRecognizer: function() {
        this.recognizer.stop();
    }
})