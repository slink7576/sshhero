import { Component, Input, Output, EventEmitter } from "@angular/core";
import { ServersService } from "src/app/services/servers.service";
import { Credentials } from "src/api";
import { FormGroup, FormBuilder } from "@angular/forms";



@Component({
    selector: 'shared-machine-selector-component',
    templateUrl: './shared-machine-selector.component.html',
    styleUrls: ['./shared-machine-selector.component.css']
  })

export class SharedMachineSelectorComponent  {
  constructor(private formBuilder: FormBuilder, private serversService: ServersService){}

  servers = new Array<Credentials>();
  serversFormGroup: FormGroup;

  @Output() onChangeServer = new EventEmitter<Credentials>();

  onChangedServer(){
    this.onChangeServer.emit(this.serversFormGroup.controls['Server'].value);
  }

  ngOnInit() {
    this.serversFormGroup = this.formBuilder.group({
      Server: ['', ]
    });
    this.servers = this.serversService.getServers();
    if(this.servers.length != 0){
      this.serversFormGroup.controls['Server'].setValue(this.servers[0]);
      this.onChangeServer.emit(this.serversFormGroup.controls['Server'].value);
    }
  }
}