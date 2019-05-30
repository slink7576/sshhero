import { Component, OnInit } from "@angular/core";
import { ServersService } from "src/app/services/servers.service";
import { Credentials } from "src/api";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";

@Component({
    selector: 'settings-component',
    templateUrl: './settings.component.html',
    styleUrls: ['./settings.component.css']
  })

export class SettingsComponent implements OnInit{
  servers: Array<Credentials>;
  serverFormGroup: FormGroup;
  constructor(private formBuilder: FormBuilder, private serversService:ServersService){}
  ngOnInit(){
    this.servers = this.serversService.getServers();
    this.serverFormGroup =  this.formBuilder.group({
      hostName: ['', Validators.required],
      login: ['', Validators.required],
      password: ['', Validators.required]
    });
  }
  onSubmitServer(){
    if(this.serverFormGroup.valid){
      let serv = new Credentials();
      serv.hostname = this.serverFormGroup.controls['hostName'].value;
      serv.login = this.serverFormGroup.controls['login'].value;
      serv.password = this.serverFormGroup.controls['password'].value;
      this.serversService.addServer(serv);
      this.servers = this.serversService.getServers();
  }
 
}
onDeleteServer(server){
  console.log(server)
  this.serversService.deleteServer(server);
  this.servers = this.serversService.getServers();
}
}