function isValidEmailAddress(email) {
    var reg = /^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$/;
    if (reg.test(email)) {
        return true;
    }
    else {
        return false;
    }
}