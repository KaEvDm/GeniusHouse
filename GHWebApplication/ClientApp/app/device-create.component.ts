import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DataService } from './data.service';
import { Device } from './device';

@Component({
    templateUrl: './device-create.component.html'
})
export class DeviceCreateComponent {

    device: Device = new Device();    // добавляемый объект
    constructor(private dataService: DataService, private router: Router) { }
    save() {
        this.dataService.createDevice(this.device).subscribe(data => this.router.navigateByUrl("/"));
    }
}