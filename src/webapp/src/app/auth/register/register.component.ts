import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticationService } from '../auth.service';
import { LoginModel } from '../models/login-model';
import { FormGroup, FormControl } from '@angular/forms';
import { RegisterModel } from '../models/register-model';

@Component({
    selector: 'app-login',
    templateUrl: './register.component.html',
    styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
    userForm: FormGroup = new FormGroup({
        fullName: new FormControl(''),
        email: new FormControl(''),
        phone: new FormControl(''),
        password: new FormControl('')
    });

    constructor(private router: Router, private auth: AuthenticationService) { }

    ngOnInit() { }

    onFormSubmit() {
        let model: RegisterModel = {
            FullName: this.userForm.get('fullName').value,
            Email: this.userForm.get('email').value,
            Phone: this.userForm.get('phone').value,
            Password: this.userForm.get('password').value
        };

        this.auth.register(model).subscribe(
            res => {
                if (res) {
                    this.router.navigate(['/login']);
                }
            }
        )
    }
}
