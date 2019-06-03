import { Component, OnInit, ViewChild, OnDestroy } from "@angular/core";
import { CommandClient, ProcessInfo, Credentials, GetAllProcessesCommand, KillProcessCommand } from "src/api";
import { MatTableDataSource, MatSort } from "@angular/material";

@Component({
    selector:'processes-component', 
    templateUrl: './processes.component.html',
    styleUrls: ['./processes.component.css']
})

export class ProcessesComponent implements OnInit, OnDestroy{

    processes = new Array<ProcessInfo>();
    currentServer = new Credentials();
    error = '';
    displayedColumns: string[] = ['id', 'name', 'cpu', 'memory', 'kill'];
    dataSource: MatTableDataSource<ProcessInfo>;
    refreshProcessInterval: any;
    @ViewChild(MatSort) sort: MatSort;

    constructor(private client: CommandClient){}

    ngOnDestroy() {
        clearInterval(this.refreshProcessInterval);
    }

    ngOnInit(){
        this.refreshProcessInterval = setInterval(() => {
            let comm = new GetAllProcessesCommand();
            comm.credentials = new Credentials(this.currentServer);
            this.client.getAllProcesses(comm).subscribe(data => {
                if(!data.isError){
                    this.error = '';
                    this.processes = data.processes;
                    this.dataSource = new MatTableDataSource(this.processes);
                    this.dataSource.sort = this.sort;
                }else{
                    this.processes = new Array<ProcessInfo>();
                    this.error = data.error;
                }
            });
        },2000);
    }
    onChangeServer(event){
        this.currentServer = event;
        let comm = new GetAllProcessesCommand();
        comm.credentials = new Credentials(this.currentServer);
        this.client.getAllProcesses(comm).subscribe(data => {
            if(!data.isError){
                this.error = '';
                this.processes = data.processes;
                this.dataSource = new MatTableDataSource(this.processes);
                this.dataSource.sort = this.sort;
            }else{
                this.processes = new Array<ProcessInfo>();
                this.error = data.error;
            }
        });
    }
    onKillProcess(id){
        let command = new KillProcessCommand();
        command.credentials = new Credentials(this.currentServer);
        command.id = id;
        this.client.killProcess(command).subscribe(data =>{
            if(!data.isError){
                let elem = this.processes.find(el => el.id == id);
                let ind = this.processes.indexOf(elem);
                this.processes.splice(ind, 1);
                this.dataSource = new MatTableDataSource(this.processes);
            }
        });
    }
}