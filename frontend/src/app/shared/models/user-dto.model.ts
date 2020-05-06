export class UserDtoModel {
    id: number;
    userName: string;
    email: string;
    newPassword: string;
    oldPassword: string;
    confirmPassword: string;
    roles: string[];
}
