CREATE DATABASE QLCaPhe;
GO

-- Chuyển sang cơ sở dữ liệu QLCaPhe
USE QLCaPhe;
GO
create table TableFood (
id int identity  primary key,
ten nvarchar(100),
status nvarchar(100),
TenKH nvarchar(100)
);
go
CREATE PROCEDURE Them_DATA_VAO_TABLE
    @a INT,@Tenkh nvarchar(100)
AS  
BEGIN  
    -- Cập nhật tên và trạng thái của bàn nếu có id = @a  
    UPDATE [dbo].[TableFood]  
    SET ten = N'Bàn ' + CAST(@a AS NVARCHAR(100)), status = N'Có người',TenKH=@Tenkh
    WHERE id = @a;  
END  
go
CREATE PROCEDURE XOA_TABLE
    @Idban INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;
    BEGIN TRY
        -- Xóa dòng có Idban tương ứng
        DELETE FROM [TableFood] WHERE [TableFood].id = @Idban;

        -- Lấy giá trị MAX(id) còn lại trong bảng
        DECLARE @MaxID INT;
        SELECT @MaxID = MAX(id) FROM [TableFood];

        -- Đặt lại giá trị IDENTITY nếu bảng không rỗng
        IF @MaxID IS NOT NULL
            DBCC CHECKIDENT ('TableFood', RESEED, @MaxID);
        ELSE
            -- Nếu bảng rỗng, reset về 0
            DBCC CHECKIDENT ('TableFood', RESEED, 0);

        -- Commit transaction
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Rollback nếu xảy ra lỗi
        ROLLBACK TRANSACTION;

        -- Ném ra lỗi để ứng dụng biết
        THROW;
    END CATCH
END;
GO
CREATE PROCEDURE INSERT_IN_FIRST_AVAILABLE_TABLE2
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;
    BEGIN TRY
        -- Tìm giá trị nhỏ nhất chưa sử dụng cho ID bàn
        DECLARE @NextID INT;
        SELECT @NextID = MIN(A.id + 1)
        FROM TableFood A
        LEFT JOIN TableFood B ON A.id + 1 = B.id
        WHERE B.id IS NULL;

        -- Nếu không tìm thấy, chèn vào cuối
        IF @NextID IS NULL
            SELECT @NextID = ISNULL(MAX(id), 0) + 1 FROM TableFood;

        -- Tên bàn tự động
        DECLARE @TenBan NVARCHAR(100);
        SET @TenBan = N'Bàn ' + CAST(@NextID AS NVARCHAR(100));

        -- Cho phép chèn thủ công vào cột IDENTITY
        SET IDENTITY_INSERT TableFood ON;

        -- Chèn dữ liệu mới với ID đã xác định, status mặc định là 'Trống', TenKH là NULL
        INSERT INTO TableFood (id, ten, status, TenKH)
        VALUES (@NextID, @TenBan, N'Trống', NULL);

        -- Tắt chế độ chèn thủ công
        SET IDENTITY_INSERT TableFood OFF;

        -- Commit transaction
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Rollback nếu có lỗi
        ROLLBACK TRANSACTION;

        -- Ném lỗi ra ngoài
        THROW;
    END CATCH
END;
GO

create proc GetTableList 
as select * from  [dbo].[TableFood]
go
DBCC CHECKIDENT ('TableFood', RESEED, 0);
GO
create proc Get_ALL_Table 
as select * from  [dbo].[TableFood]
go
CREATE PROCEDURE XOA_MON_CUOI_CUNG_TRONG_LIST_VIEW
    @a INT
AS
BEGIN
    -- Cập nhật tên và trạng thái của bàn nếu có id = @a
    UPDATE [dbo].[TableFood]
    SET ten = N'Bàn ' + CAST(@a AS NVARCHAR(100)), status = N'Trống',TenKH=NULL
    WHERE id = @a;
END
GO
CREATE PROCEDURE XOA_TABLE
    @Idban INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;
    BEGIN TRY
        -- Xóa dòng có Idban tương ứng
        DELETE FROM [TableFood] WHERE [TableFood].id = @Idban;

        -- Lấy giá trị MAX(id) còn lại trong bảng
        DECLARE @MaxID INT;
        SELECT @MaxID = MAX(id) FROM [TableFood];

        -- Đặt lại giá trị IDENTITY nếu bảng không rỗng
        IF @MaxID IS NOT NULL
            DBCC CHECKIDENT ('TableFood', RESEED, @MaxID);
        ELSE
            -- Nếu bảng rỗng, reset về 0
            DBCC CHECKIDENT ('TableFood', RESEED, 0);

        -- Commit transaction
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Rollback nếu xảy ra lỗi
        ROLLBACK TRANSACTION;

        -- Ném ra lỗi để ứng dụng biết
        THROW;
    END CATCH
END;
GO
CREATE PROCEDURE ThemTableList  
    @a INT  
AS  
BEGIN  
    -- Cập nhật tên và trạng thái của bàn nếu có id = @a  
    UPDATE [dbo].[TableFood]  
    SET ten = N'Bàn ' + CAST(@a AS NVARCHAR(100)), status = N'Có người'  
    WHERE id = @a;  
END  
go
CREATE PROCEDURE Them_DATA_VAO_TABLE
    @a INT,@Tenkh nvarchar(100)
AS  
BEGIN  
    -- Cập nhật tên và trạng thái của bàn nếu có id = @a  
    UPDATE [dbo].[TableFood]  
    SET ten = N'Bàn ' + CAST(@a AS NVARCHAR(100)), status = N'Có người',TenKH=@Tenkh
    WHERE id = @a;  
END  
go
--------------------------------------------------------

CREATE TABLE     DANHMUC_DOUONG
(
IDDM INT IDENTITY(1,1),
TENDM NVARCHAR(50) NOT NULL,
CONSTRAINT PK_IDDM PRIMARY KEY(IDDM)
)
GO
CREATE PROCEDURE LOAD_DANHMUC
AS
BEGIN
SELECT * FROM [dbo].[DOUONG]
END
GO
CREATE PROCEDURE INSERT_DANHMUC
@TENDM NVARCHAR(50)
AS
BEGIN
INSERT INTO DANHMUC_DOUONG(TENDM) VALUES(@TENDM)
END
GO
CREATE PROCEDURE UPDATE_DANHMUC
@IDDM INT,@TENDM NVARCHAR(50)
AS
BEGIN
UPDATE DANHMUC_DOUONG SET TENDM=@TENDM WHERE IDDM=@IDDM
END
GO
CREATE PROCEDURE DELETE_DANHMUC
@IDDM INT
AS
BEGIN
DELETE FROM DANHMUC_DOUONG WHERE IDDM=@IDDM
END
GO
insert into DANHMUC_DOUONG (TENDM)
values (N'Đồ ăn');
go
------------------------------------------------------------

CREATE TABLE  DOUONG
(
IDDOUONG INT IDENTITY(1,1),
IDDM INT NOT NULL,
TENDOUONG NVARCHAR(100) NOT NULL,
DONGIA FLOAT DEFAULT 0 NOT NULL,
CONSTRAINT PK_IDDOUONG PRIMARY KEY(IDDOUONG),
CONSTRAINT FK_IDDM FOREIGN KEY(IDDM) REFERENCES DANHMUC_DOUONG(IDDM)
)
GO
CREATE PROCEDURE INSERT_IN_FIRST_AVAILABLE_DO_UONG 
    @IDDM INT,               -- ID danh mục phải được cung cấp  
    @TENDOUONG NVARCHAR(100),  
    @DONGIA FLOAT  
AS  
BEGIN  
    SET NOCOUNT ON;  
  
    BEGIN TRANSACTION;  
    BEGIN TRY  
        -- Tìm giá trị nhỏ nhất chưa sử dụng  
        DECLARE @NextID INT;  
        SELECT @NextID = MIN(A.IDDOUONG + 1)  
        FROM DOUONG A  
        LEFT JOIN DOUONG B ON A.IDDOUONG + 1 = B.IDDOUONG  
        WHERE B.IDDOUONG IS NULL;  
  
        -- Nếu không tìm thấy, chèn vào cuối  
        IF @NextID IS NULL  
            SELECT @NextID = ISNULL(MAX(IDDOUONG), 0) + 1 FROM DOUONG;  
  
        -- Cho phép chèn thủ công vào cột IDENTITY  
        SET IDENTITY_INSERT DOUONG ON;  
  
        -- Chèn dữ liệu mới với ID đã xác định  
        INSERT INTO DOUONG (IDDOUONG, IDDM, TENDOUONG, DONGIA)  
        VALUES (@NextID, @IDDM, @TENDOUONG, @DONGIA);  
  
        -- Tắt chế độ chèn thủ công  
        SET IDENTITY_INSERT DOUONG OFF;  
  
        -- Commit transaction  
        COMMIT TRANSACTION;  
    END TRY  
    BEGIN CATCH  
        -- Rollback nếu có lỗi  
        ROLLBACK TRANSACTION;  
  
        -- Ném lỗi ra ngoài  
        THROW;  
    END CATCH  
END
GO
CREATE PROCEDURE LOAD_DOUONG
AS
BEGIN
SELECT DOUONG.IDDOUONG,DOUONG.IDDM,DANHMUC_DOUONG.TENDM,DOUONG.TENDOUONG,DOUONG.DONGIA FROM DOUONG inner join DANHMUC_DOUONG on DANHMUC_DOUONG.IDDM=DOUONG.IDDM
END
GO

CREATE PROCEDURE LOAD_DOUONG_ID
@IDDM INT
AS
BEGIN
SELECT TENDOUONG,DONGIA FROM DOUONG WHERE IDDM=@IDDM
END
GO

CREATE PROCEDURE LOAD_INDSDOUONG
AS
BEGIN
SELECT DOUONG.TENDOUONG,DOUONG.DONGIA FROM DOUONG
END
GO

CREATE PROCEDURE UPDATE_DOUONG2
@IDDOUONG INT,@TENDOUONG NVARCHAR(100),@DONGIA FLOAT
AS
BEGIN
UPDATE DOUONG SET TENDOUONG=@TENDOUONG,DONGIA=@DONGIA WHERE IDDOUONG=@IDDOUONG
END
GO

CREATE PROCEDURE DELETE_DOUONG3
    @IDDOUONG INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;
    BEGIN TRY
        -- Xóa dòng có IDDOUONG tương ứng
        DELETE FROM DOUONG WHERE IDDOUONG = @IDDOUONG;

        -- Lấy giá trị MAX(IDDOUONG) còn lại trong bảng
        DECLARE @MaxID INT;
        SELECT @MaxID = MAX(IDDOUONG) FROM DOUONG;

        -- Đặt lại giá trị IDENTITY nếu bảng không rỗng
        IF @MaxID IS NOT NULL
            DBCC CHECKIDENT ('DOUONG', RESEED, @MaxID);
        ELSE
            -- Nếu bảng rỗng, reset về 0
            DBCC CHECKIDENT ('DOUONG', RESEED, 0);

        -- Commit transaction
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Rollback nếu xảy ra lỗi
        ROLLBACK TRANSACTION;

        -- Ném ra lỗi để ứng dụng biết
        THROW;
    END CATCH
END;
GO
---------------------------------------------------

CREATE TABLE HOADON
(
IDHOADON INT IDENTITY(1,1),
IDBAN INT NOT NULL,
TenMon nvarchar(20),
SoLuong int,
DonGia int,
ThanhTien int,
CONSTRAINT PK_IDHOADON PRIMARY KEY(IDHOADON),
CONSTRAINT FK_IDBANHD FOREIGN KEY(IDBAN) REFERENCES TableFood(id)
)
GO
CREATE PROCEDURE SP_INSERT_OR_UPDATE_IN_FIRST_AVAILABLE_HOA_DON
    @IDBAN INT,            -- ID bàn
    @TenMon NVARCHAR(20),  -- Tên món ăn
    @SoLuong INT,          -- Số lượng món
    @DonGia INT            -- Đơn giá
AS
BEGIN
    SET NOCOUNT ON;
    DECLARE @ThanhTien INT = @SoLuong * @DonGia; -- Tính thành tiền

    BEGIN TRANSACTION; -- Bắt đầu giao dịch

    BEGIN TRY
        -- 1. Kiểm tra trùng dữ liệu trong bảng
        IF EXISTS (SELECT 1 FROM HOADON WHERE IDBAN = @IDBAN AND TenMon = @TenMon)
        BEGIN
            -- Cập nhật dữ liệu nếu trùng
            UPDATE HOADON
            SET SoLuong = SoLuong + @SoLuong,
                ThanhTien = ThanhTien + @ThanhTien
            WHERE IDBAN = @IDBAN AND TenMon = @TenMon;
        END
        ELSE
        BEGIN
            -- 2. Tìm IDHOADON tiếp theo còn trống
            DECLARE @NextID INT;

            SELECT @NextID = MIN(A.IDHOADON + 1)
            FROM HOADON A
            LEFT JOIN HOADON B ON A.IDHOADON + 1 = B.IDHOADON
            WHERE B.IDHOADON IS NULL;

            -- Nếu không tìm thấy khoảng trống, chèn vào ID kế tiếp
            IF @NextID IS NULL
                SELECT @NextID = ISNULL(MAX(IDHOADON), 0) + 1 FROM HOADON;

            -- Bật chế độ cho phép chèn thủ công vào IDENTITY
            SET IDENTITY_INSERT HOADON ON;

            -- Chèn dữ liệu mới vào bảng
            INSERT INTO HOADON (IDHOADON, IDBAN, TenMon, SoLuong, DonGia, ThanhTien)
            VALUES (@NextID, @IDBAN, @TenMon, @SoLuong, @DonGia, @ThanhTien);

            -- Tắt chế độ chèn IDENTITY
            SET IDENTITY_INSERT HOADON OFF;
        END

        -- Commit transaction khi thành công
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Rollback nếu có lỗi
        ROLLBACK TRANSACTION;

        -- Ném lỗi ra ngoài
        THROW;
    END CATCH
END;
GO
CREATE PROCEDURE DELETE_HOA_DON_VOI_ID
    @IDhoaDon INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;
    BEGIN TRY
        -- Xóa dòng có IDDOUONG tương ứng
        DELETE FROM [dbo].[HOADON] WHERE [dbo].[HOADON].IDHOADON  = @IDhoaDon;

        -- Lấy giá trị MAX(IDDOUONG) còn lại trong bảng
        DECLARE @MaxID INT;
        SELECT @MaxID = MAX([IDHOADON]) FROM [dbo].[HOADON];

        -- Đặt lại giá trị IDENTITY nếu bảng không rỗng
        IF @MaxID IS NOT NULL
            DBCC CHECKIDENT ('HOADON', RESEED, @MaxID);
        ELSE
            -- Nếu bảng rỗng, reset về 0
            DBCC CHECKIDENT ('HOADON', RESEED, 0);

        -- Commit transaction
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Rollback nếu xảy ra lỗi
        ROLLBACK TRANSACTION;

        -- Ném ra lỗi để ứng dụng biết
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE LOAD_HOA_DON_NEW
AS
BEGIN
select * from HOADON
END
GO

CREATE PROCEDURE EDIT_HOA_DON_NEW
    @IDHD INT,
    @TenMon NVARCHAR(20),
    @SoLuong INT,
    @DonGia INT,
    @ThanhTien INT
AS
BEGIN
    -- Cập nhật dữ liệu trong bảng HOADON
    UPDATE HOADON
    SET
        TenMon = @TenMon,
        SoLuong = @SoLuong,
        DonGia = @DonGia,
        ThanhTien = @ThanhTien
    WHERE IDHOADON = @IDHD; -- Điều kiện cập nhật dựa trên IDBAN
END;
GO


CREATE PROCEDURE DELETE_HOA_DON3
    @idHD INT
AS
BEGIN
    DELETE FROM HOADON
    WHERE HOADON.IDHOADON = @idHD; -- Sử dụng đúng tham số @idban
END;
GO

------------------------------------------------------------

CREATE TABLE NHANVIEN
(
IDNV char(20),
TENNV NVARCHAR(100) NOT NULL,
NGAYSINH DATE NOT NULL,
SDT CHAR(12) NOT NULL,
GIOITINH NCHAR(3) NOT NULL
CONSTRAINT PK_IDNV PRIMARY KEY(IDNV)
)
GO
CREATE PROCEDURE INSERT_NHANVIEN
    @TENNV NVARCHAR(100), 
    @NGAYSINH DATE, 
    @SDT CHAR(12), 
    @GIOITINH NCHAR(3)
AS
BEGIN
    INSERT INTO NHANVIEN (TENNV, NGAYSINH, SDT, GIOITINH) 
    VALUES (@TENNV, @NGAYSINH, @SDT, @GIOITINH)
END
---------------------------------------------------------

CREATE TABLE TAIKHOAN
(
TENTK VARCHAR(50),
MATKHAU VARCHAR(20) NOT NULL,
QUYEN VARCHAR(20) NOT NULL CHECK (QUYEN='NHANVIEN' OR QUYEN='ADMIN'),
IDNV int NOT NULL,
CONSTRAINT PK_TENTK PRIMARY KEY(TENTK),
CONSTRAINT FK_IDNVTK FOREIGN KEY(IDNV) REFERENCES NHANVIEN(IDNV)
)
GO


CREATE PROCEDURE LOADTAIKHOANVANHANVIEN4 
AS
BEGIN
   select a.TENTK,a.MATKHAU,a.QUYEN,b.TENNV,b.GIOITINH,b.NGAYSINH,b.SDT,b.IDNV
   from [dbo].[TAIKHOAN] a
   join [dbo].[NHANVIEN] b on a.IDNV = b.IDNV
    
END
go
CREATE PROCEDURE LOAD_TK_NHAN_VIEN_DE_TIM
AS
BEGIN
    select a.IDNV AS IDNV_A, a.TENTK, a.MATKHAU, a.QUYEN,
           b.TENNV, b.GIOITINH, b.NGAYSINH, b.SDT, b.IDNV AS IDNV_B
    from [dbo].[TAIKHOAN] a
    join [dbo].[NHANVIEN] b on a.IDNV = b.IDNV
END

CREATE PROCEDURE DELETE_NHAN_VIEN_VA_TK
    @IDNV INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;
    BEGIN TRY
        -- Xóa tài khoản liên quan đến nhân viên
        DELETE FROM TAIKHOAN WHERE IDNV = @IDNV;

        -- Xóa nhân viên
        DELETE FROM NHANVIEN WHERE IDNV = @IDNV;

        -- Lấy giá trị MAX(IDNV) còn lại trong bảng NHANVIEN
        DECLARE @MaxID INT;
        SELECT @MaxID = MAX(IDNV) FROM NHANVIEN;

        -- Đặt lại giá trị IDENTITY nếu bảng không rỗng
        IF @MaxID IS NOT NULL
            DBCC CHECKIDENT ('NHANVIEN', RESEED, @MaxID);
        ELSE
            -- Nếu bảng NHANVIEN rỗng, reset về 0
            DBCC CHECKIDENT ('NHANVIEN', RESEED, 0);

        -- Commit transaction
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Rollback nếu xảy ra lỗi
        ROLLBACK TRANSACTION;

        -- Ném lỗi ra để ứng dụng biết
        THROW;
    END CATCH
END
GO

CREATE PROCEDURE INSERT_IN_FIRST_AVAILABLE_POSITION_NV_TK
    @TENNV NVARCHAR(100),    -- Tên nhân viên
    @NGAYSINH DATE,          -- Ngày sinh
    @SDT CHAR(12),           -- Số điện thoại
    @GIOITINH NCHAR(3),      -- Giới tính
    @TENTK VARCHAR(50),      -- Tên tài khoản
    @MATKHAU VARCHAR(20),    -- Mật khẩu
    @QUYEN VARCHAR(20)       -- Quyền ('NHANVIEN' hoặc 'ADMIN')
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;
    BEGIN TRY
        -- Tìm IDNV nhỏ nhất bị thiếu trong bảng NHANVIEN
        DECLARE @NextIDNV INT;
        SELECT @NextIDNV = MIN(A.IDNV + 1)
        FROM NHANVIEN A
        LEFT JOIN NHANVIEN B ON A.IDNV + 1 = B.IDNV
        WHERE B.IDNV IS NULL;

        -- Nếu không tìm thấy IDNV trống, chèn vào cuối
        IF @NextIDNV IS NULL
            SELECT @NextIDNV = ISNULL(MAX(CAST(IDNV AS INT)), 0) + 1 FROM NHANVIEN;

        -- Bật chế độ IDENTITY_INSERT để chèn giá trị vào cột IDNV
        SET IDENTITY_INSERT NHANVIEN ON;

        -- Chèn dữ liệu vào bảng NHANVIEN
        INSERT INTO NHANVIEN (IDNV, TENNV, NGAYSINH, SDT, GIOITINH)
        VALUES (CAST(@NextIDNV AS CHAR(20)), @TENNV, @NGAYSINH, @SDT, @GIOITINH);

        -- Tắt chế độ IDENTITY_INSERT sau khi chèn xong
        SET IDENTITY_INSERT NHANVIEN OFF;

        -- Chèn dữ liệu vào bảng TAIKHOAN
        INSERT INTO TAIKHOAN (TENTK, MATKHAU, QUYEN, IDNV)
        VALUES (@TENTK, @MATKHAU, @QUYEN, @NextIDNV);

        -- Commit transaction nếu mọi thứ thành công
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Rollback transaction nếu xảy ra lỗi
        ROLLBACK TRANSACTION;

        -- Ném lỗi ra ngoài
        THROW;
    END CATCH
END
GO
CREATE PROCEDURE UPDATE_NV_TK
    @idNV INT,
    @TENNV NVARCHAR(100),    -- Tên nhân viên
    @NGAYSINH DATE,          -- Ngày sinh
    @SDT CHAR(12),           -- Số điện thoại
    @GIOITINH NCHAR(3),      -- Giới tính
    @TENTK VARCHAR(50),      -- Tên tài khoản
    @MATKHAU VARCHAR(20),    -- Mật khẩu
    @QUYEN VARCHAR(20)       -- Quyền ('NHANVIEN' hoặc 'ADMIN')
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRANSACTION;
    BEGIN TRY
        -- Cập nhật thông tin nhân viên trong bảng NHANVIEN
        UPDATE [dbo].[NHANVIEN]
        SET 
            TENNV = @TENNV,
            NGAYSINH = @NGAYSINH,
            SDT = @SDT,
            GIOITINH = @GIOITINH
        WHERE IDNV = @idNV;

        -- Cập nhật thông tin tài khoản trong bảng TAIKHOAN
        UPDATE [dbo].[TAIKHOAN]
        SET 
            MATKHAU = @MATKHAU,
            QUYEN = @QUYEN
        WHERE IDNV = @idNV;

        -- Commit transaction nếu mọi thứ thành công
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        -- Rollback transaction nếu có lỗi
        ROLLBACK TRANSACTION;

        -- Ném lỗi ra ngoài
        THROW;
    END CATCH
END;
GO

create procedure LOAD_TK_DE_LOGIN3
as 
begin 
    select a.TENTK, a.MATKHAU, a.QUYEN, b.TENNV, b.IDNV
    from [dbo].[TAIKHOAN] a
    join [dbo].[NHANVIEN] b on a.IDNV = b.IDNV
end; 

---------------------------------------------------------------
CREATE TABLE HOADON_OLD
(
IDHD_OLD INT IDENTITY(1,1),
IDBAN INT NOT NULL,
TENNV nvarchar(30) NOT NULL,
TENMONAN nvarchar(100) NOT NULL,
TENKH nvarchar(100) NOT NULL,
NGAYLAP DATE ,
TONGTIEN INT DEFAULT 0 NOT NULL
CONSTRAINT PK_IDHD_OLD PRIMARY KEY(IDHD_OLD),
CONSTRAINT FK_IDBAN FOREIGN KEY(IDBAN) REFERENCES TableFood(id),
)
GO
CREATE PROCEDURE INSERT_HOADON_THANH_TOAN
@IDBAN INT ,@TENNV nvarchar(30),@TENMONAN nvarchar(100),@TENKH nvarchar(100),@NGAYLAP DATE,@TONGTIEN FLOAT
AS
BEGIN
INSERT INTO HOADON_OLD(IDBAN,TENNV,TENMONAN,TENKH ,NGAYLAP,TONGTIEN) VALUES(@IDBAN,@TENNV,@TENMONAN,@TENKH,@NGAYLAP,@TONGTIEN)
END
GO
CREATE PROCEDURE LOAD_HOA_DON_THANH_TOAN
AS
BEGIN
   select IDBAN,TENNV,TENMONAN,TONGTIEN,TENKH,NGAYLAP
   from HOADON_OLD 
END
insert into DANHMUC_DOUONG (TENDM)
values (N'Nước'),
 (N'Đồ ăn');
go