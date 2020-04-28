
export class Student {
    id: number;
    firstName = '';
    lastName = '';
    patronymic = '';
    groupId: number;
    fullName = this.lastName + ' ' + this.firstName;
}
