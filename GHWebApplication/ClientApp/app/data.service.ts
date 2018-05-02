import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Device } from './device';

@Injectable()
export class DataService
{

    private url = "/api/device/";

    constructor(private http: HttpClient) { }

    getDevices()
    {
        return this.http.get(this.url + "GetAll");
    }

    getDevice(id: number) {
        return this.http.get(this.url + "GetOne/" + id);
    }

    getDevicesSort(room?: string, category?: string) {
        //?параметр1=значение1&параметр2=значение2
        return this.http.get(this.url + "GetSort?room=" + room + "&category=" + category);
    }

    createDevice(device: Device)
    {
        return this.http.post(this.url + "AddNew", device, { observe: 'response' });
    }

    updateDevice(device: Device)
    {
        return this.http.put(this.url + "Update/" + device.id, device);
    }

    deleteDevice(id: number)
    {
        return this.http.delete(this.url + "Delete/" + id);
    }
}