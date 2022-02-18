# Library

Software gestionale per una biblioteca realizzato nell'Academy CRM tramite Avanade Italy srl. Sono state usate le seguenti tecnologie: 
- c#, 
- html, 
- css, 
- bootstrap, 
- javascript (jquery), 
- MSS (DBMS - Microsoft SQL Server)
- web service per la comunicazione tra diversi sistemi, perché inizialmente l'applicazione è stata creata come console application per poi usare le business logics create, come servizi esterni disponibili all'utilizzo nella nuova applicazione MVC.

--------------------------------------------------------------------------------------------------------------------------------------------------------------------
CHE COSA FA?


L’applicazione permette di interfacciare l’utente con i contenuti della biblioteca, i quali sono resi disponibili mediante una banca dati. 
L'applicazione prevede l’accesso per due tipi distinti di utenti: utilizzatori e amministratori. 
Gli utenti utilizzatori sono in grado di accedere a una lista di libri disponibili e richiederne il prestito, nonché di formalizzarne la restituzione in qualsiasi momento rendendo di nuovo disponibile il titolo. 

Agli amministratori dell’applicazione è permesso tutto ciò che è permesso agli utilizzatori, con in più la possibilità di manipolare la banca dati, aggiungendo libri alla stessa, rimuovendone (a patto che non siano in prestito)
o modificandone i dettagli. 
 
Le funzionalità necessarie al corretto funzionamento del sistema, e dunque implementate a livello di Business Logic, sono le seguenti: 


- Login

- Inserimento di un libro 

- Modifica di un libro

- Cancellazione di un libro 

- Ricerca di un libro  

- Prestito di un libro 

- Restituzione del libro 

- Uscita dall’applicativo 

