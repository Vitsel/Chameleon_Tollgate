package com.chameleon.tollgate.usb;

public class AuthStatus {
	private String id;
	private boolean verified;
	private boolean succes;
	
	public AuthStatus(String id) {
		this.id = id;
		this.verified = false;
		this.succes = false;
	}
	
	public void setId(String id) {
		this.id = id;
	}
	
	public String getId() {
		return this.id;
	}
	
	public void setVerified(boolean verified) {
		this.verified = verified;
	}
	
	public boolean isVerified() {
		return this.verified;
	}
	
	public void setSuccess(boolean success) {
		this.succes = success;
	}
	
	public boolean isSuccess() {
		return this.succes;
	}
}
