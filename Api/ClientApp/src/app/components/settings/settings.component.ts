import { Component, OnInit } from "@angular/core";
import { ServersService } from "src/app/services/servers.service";
import { Credentials, CommandClient, CheckConnectionCommand, CheckConnectionViewModel } from "src/api";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { ServerViewModel } from "src/entities/ServerViewModel";


@Component({
  selector: 'settings-component',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})

export class SettingsComponent implements OnInit {
  servers = new Array<ServerViewModel>();
  statuses = new Array<boolean>();
  serverFormGroup: FormGroup;
  constructor(private formBuilder: FormBuilder,
    private client: CommandClient, private serversService: ServersService) { }
  ngOnInit() {
    this.serversService.getServers().forEach(element => {
      let command = new CheckConnectionCommand();
      let credentials = new Credentials(element);
      command.credentials = credentials;
      this.client.checkConnection(command).subscribe(data =>{
        let obj = new ServerViewModel();
        obj.isAlive = data.isAlive;
        obj.credentials = credentials;
        this.servers.push(obj);
      });
      
    });;
    this.serverFormGroup = this.formBuilder.group({
      hostName: ['', Validators.required],
      login: ['', Validators.required],
      password: ['', Validators.required]
    });
  }
  onSubmitServer() {
    if (this.serverFormGroup.valid) {
      let serv = new Credentials();
      serv.hostname = this.serverFormGroup.controls['hostName'].value;
      serv.login = this.serverFormGroup.controls['login'].value;
      serv.password = this.serverFormGroup.controls['password'].value;
      this.serverFormGroup.reset();
      this.serversService.addServer(serv);
      let comm = new CheckConnectionCommand();
      comm.credentials = serv;
      this.client.checkConnection(comm).subscribe(data => {
        let obj = new ServerViewModel();
        obj.isAlive = data.isAlive;
        obj.credentials = serv;
         this.servers.push(obj);
      });
    }
  }
  onDeleteServer(server: ServerViewModel, index: number) {
    this.serversService.deleteServer(server.credentials);
    this.servers.splice(index, 1);
  }
}