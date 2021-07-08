package com.chameleon.tollgate.rest;

import java.util.ArrayList;

import com.chameleon.tollgate.rest.exception.AuthError;
import com.chameleon.tollgate.rest.exception.NoUserException;

public class SessionList {
	private ArrayList<SessionTime> sessions;
	
	public SessionList() {
		this.sessions = new ArrayList<SessionTime>();
	}
	
	public void add(String id, int timestamp) {
		SessionTime session = new SessionTime(id, timestamp);
		sessions.add(session);
	}
	
	@SuppressWarnings("finally")
	public void remove(String id) {
		try {
			SessionTime session = findSession(id);
			this.sessions.remove(session);
		} finally {
			return;
		}
	}
	
	public int getTimestamp(String id) throws NoUserException {
		SessionTime session = findSession(id);
		return session.getTimestamp();
	}
	
	public SessionTime findSession(String id) throws NoUserException {
		for(SessionTime session : this.sessions) {
			if(session.getId().equals(id)) {
				return session;
			}
		}
		throw new NoUserException(AuthError.NO_USER);
	}
	
	public boolean isExist(SessionTime session) throws NoUserException {
		SessionTime st = findSession(session.getId());
		if(st.equals(session))
			return true;
		return false;
	}
}