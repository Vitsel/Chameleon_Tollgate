package com.chameleon.tollgate.database;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;

import org.sqlite.SQLiteConfig;

import com.chameleon.tollgate.database.exception.DatabaseConnectException;
import com.chameleon.tollgate.database.exception.DatabaseResultException;
import com.chameleon.tollgate.util.Util;
import com.chameleon.tollgate.database.define.*;

public class SQLiteManager {
	static {
		try {
			Class.forName("org.sqlite.JDBC");
		} catch (Exception ex) {
			System.out.println(ex);
		}
	}

	private static final String DATABASE_URL = "jdbc:sqlite:..\\tollgate_db.db";
	protected static final int DEFAULT_VALID_TIMEOUT = 30;

	public Connection connection;
	private boolean isOpened;

	public SQLiteManager() {
		isOpened = false;
	}

	public boolean isOpen() {
		return this.isOpened;
	}

	public boolean open() {
		return this.open(false, false);
	}

	public boolean open(boolean isReadOnly) {
		return this.open(isReadOnly, false);
	}

	public boolean open(boolean isReadOnly, boolean autoCommit) {
		if (this.isOpened)
			this.close();

		try {
			SQLiteConfig config = new SQLiteConfig();
			config.setReadOnly(isReadOnly);
			this.connection = DriverManager.getConnection(DATABASE_URL, config.toProperties());
			this.connection.setAutoCommit(autoCommit);
		} catch (SQLException ex) {
			System.out.println(ex);
			return false;
		}

		this.isOpened = true;
		return true;
	}

	public boolean close() {
		if (!this.isOpened)
			return true;

		try {
			this.connection.close();
		} catch (SQLException ex) {
			System.out.println(ex);
			return true;
		}
		return true;
	}

	public boolean isValid() {
		return this.isValid(DEFAULT_VALID_TIMEOUT);
	}

	public boolean isValid(int timeOut) {
		if (!this.isOpened)
			return false;
		try {
			return this.connection.isValid(timeOut);
		} catch (SQLException ex) {
			System.out.println(ex);
			return false;
		}
	}

	public void commit() throws Exception {
		this.connection.commit();
	}

	public String getToken(String id) throws Exception {
		if (!this.isOpened)
			throw new DatabaseConnectException("There is not connected with the database.");

		int count = getCountOf(Table.MAP_ANDROID, "id", "tester");
		if (count > 1)
			throw new DatabaseResultException("Too many tokens.");
		else if (count == 0)
			throw new DatabaseResultException("There are no tokens.");

		PreparedStatement state = connection
				.prepareStatement("select token from " + Table.MAP_ANDROID + " where id = ?");
		state.setString(1, id);
		ResultSet rs = state.executeQuery();

		String result = rs.getString("token");
		rs.close();
		return result;
	}

	public int getCountOf(Table table, String column, String value) throws Exception {
		if (!this.isOpened)
			throw new DatabaseConnectException("There is not connected with the database.");
		PreparedStatement state = connection
				.prepareStatement("select count(*) from " + table + " where " + column + " = ?");
		state.setString(1, value);
		ResultSet rs = state.executeQuery();

		int result = rs.getInt(1);
		rs.close();
		return result;
	}

	public boolean isInitFactor(String id, Factor factor) throws Exception {
		if (!this.isOpened)
			throw new DatabaseConnectException("There is not connected with the database.");
		PreparedStatement state = connection.prepareStatement("select ? from " + Table.INIT_FACTOR + " where id = ?");
		state.setString(1, factor.toString());
		state.setString(1, id);
		ResultSet rs = state.executeQuery();

		int result = rs.getInt(1);
		rs.close();
		return Util.paresBoolean(result);
	}
}