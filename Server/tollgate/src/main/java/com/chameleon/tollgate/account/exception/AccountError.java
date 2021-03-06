package com.chameleon.tollgate.account.exception;

import org.springframework.http.HttpStatus;

import com.chameleon.tollgate.rest.exception.BaseExceptionType;

import lombok.Getter;

@Getter
public enum AccountError implements BaseExceptionType {
	NO_USER("아이디가 틀렸습니다.", HttpStatus.BAD_REQUEST),
	USER_ALREADY_EXIST("이미 같은 유저가 존재합니다", HttpStatus.OK);
	
	private final String message;
	private final HttpStatus httpStatus;
	
	private AccountError(String message, HttpStatus httpStatus) {
		this.message = message;
		this.httpStatus = httpStatus;
	}
}
