import { Component } from '@angular/core';
import { ExecuteCustomCommand, CommandClient, Credentials } from 'src/api';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ServersService } from 'src/app/services/servers.service';

@Component({
  selector: 'console-component',
  templateUrl: './console.component.html',
  styleUrls: ['./console.component.css']
})

export class ConsoleComponent {
constructor(private formBuilder: FormBuilder, private serversService: ServersService,
   private client: CommandClient){}

result:string = 'Hello world!';
error:string;
index = 0;

commandsHistory = new Array<string>();
commandFormGroup: FormGroup;

servers = new Array<Credentials>();
serversFormGroup: FormGroup;

  ngOnInit() {
    this.commandFormGroup = this.formBuilder.group({
      Command: ['', ]
    });
    this.serversFormGroup = this.formBuilder.group({
      Server: ['', ]
    });
    this.servers = this.serversService.getServers();
    if(this.servers.length != 0){
      this.serversFormGroup.controls['Server'].setValue(this.servers[0]);
    }
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

    let command = new ExecuteCustomCommand();
    command.command = this.commandFormGroup.controls['Command'].value;
    this.commandFormGroup.controls['Command'].setValue('');
   
    let cred = new Credentials(this.serversFormGroup.controls['Server'].value)
    command.credentials = cred;

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

