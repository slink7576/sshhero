import { Component } from '@angular/core';
import { ExecuteCustomCommand, CommandClient, Credentials } from 'src/api';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'commands-editor-component',
  templateUrl: './commands-editor.component.html',
  styleUrls: ['./commands-editor.component.css']
})

export class CommandsEditorComponent {
constructor(private _formBuilder: FormBuilder, private client: CommandClient){}

result:string = 'Hello world!';
error:string;
index = 0;

commandsHistory = new Array<string>();
commandFormGroup: FormGroup;

servers = new Array<Credentials>();
serversFormGroup: FormGroup;

  ngOnInit() {
    this.commandFormGroup = this._formBuilder.group({
      Command: ['', ]
    });
    this.serversFormGroup = this._formBuilder.group({
      Server: ['', ]
    });
    let slink = new Credentials();
    slink.hostname = '192.168.1.33';
    slink.password = 'slinkonline2';
    slink.login = 'slink7576';
    this.serversFormGroup.controls['Server'].setValue(slink);
    this.servers.push(slink);
  }
  onClear(){
    this.result = '';
    this.error = '';
  }
  onLoadCommandUp(){
    this.index--;
    if(this.index < 0){
      this.index = 0;
    }
    this.commandFormGroup.controls['Command'].setValue(this.commandsHistory[this.index]);
  }
  onLoadCommandDown(){
    this.index++;
    if(this.index >= this.commandsHistory.length){
      this.index = this.commandsHistory.length - 1;
    }
    this.commandFormGroup.controls['Command'].setValue(this.commandsHistory[this.index]);
  }

  onSendCommand(){
    this.commandsHistory.push(this.commandFormGroup.controls['Command'].value);
    this.index = this.commandsHistory.length;
    console.log(this.commandsHistory);
    let command = new ExecuteCustomCommand();
    command.command = this.commandFormGroup.controls['Command'].value;
    this.commandFormGroup.controls['Command'].setValue('');
    let credentials = new Credentials()
    credentials.hostname = '192.168.1.33';
    credentials.password = 'slinkonline2';
    credentials.login = 'slink7576';
    command.credentials = credentials;

    this.client.executeCustom(command).subscribe(data =>{
      if(data.isError){
        this.result = '';
        this.error = data.error;
      }else{
        this.result = data.result;
        this.error = ''
      }
    });
    
  }
}

