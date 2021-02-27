import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

export interface DialogData {
    title: string;
    content: string;
}

@Component({
    selector: 'app-message-box',
    templateUrl: './message-dialog-component.html'
})
export class MessageBoxComponent {

    constructor(@Inject(MAT_DIALOG_DATA) public data: DialogData) {

    }
}