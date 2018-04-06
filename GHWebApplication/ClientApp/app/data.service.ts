import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Device } from './device';

@Injectable()
export class DataService
{

    private url = "/api/devices";

    constructor(private http: HttpClient) { }

    getDevices()
    {
        return this.http.get(this.url);
    }

    getDevice(id: number) {
        return this.http.get(this.url + '/' + id);
    }

    createDevice(device: Device)
    {
        return this.http.post(this.url, device, { observe: 'response' });
    }

    updateDevice(device: Device)
    {
        return this.http.put(this.url + '/' + device.id, device);
    }

    deleteDevice(id: number)
    {
        return this.http.delete(this.url + '/' + id);
    }
}