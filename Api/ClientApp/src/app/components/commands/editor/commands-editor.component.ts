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
result:string = 'Your commands here';
commandFormGroup: FormGroup;
  ngOnInit() {
    this.commandFormGroup = this._formBuilder.group({
      Command: ['', Validators.required]
    });
  }
  onSendCommand(){
    let command = new ExecuteCustomCommand();
    command.command = this.commandFormGroup.controls['Command'].value;
    let credentials = new Credentials()
    credentials.hostname = '192.168.1.33';
    credentials.password = 'slinkonline2';
    credentials.login = 'slink7576';
    command.credentials = credentials;

    this.client.executeCustom(command).subscribe(data =>{
      console.log(data);
      this.result = data.result;
    });
  }
}

