using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistence.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixingAttachmentsFeatureToMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conversation_Tickets_TicketId",
                table: "Conversation");

            migrationBuilder.DropForeignKey(
                name: "FK_ConversationAttachment_Conversation_ConversationId",
                table: "ConversationAttachment");

            migrationBuilder.DropForeignKey(
                name: "FK_ConversationParticipant_Conversation_ConversationId",
                table: "ConversationParticipant");

            migrationBuilder.DropForeignKey(
                name: "FK_Message_Conversation_ConversationId",
                table: "Message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Message",
                table: "Message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConversationParticipant",
                table: "ConversationParticipant");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConversationAttachment",
                table: "ConversationAttachment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conversation",
                table: "Conversation");

            migrationBuilder.RenameTable(
                name: "Message",
                newName: "Messages");

            migrationBuilder.RenameTable(
                name: "ConversationParticipant",
                newName: "ConversationParticipants");

            migrationBuilder.RenameTable(
                name: "ConversationAttachment",
                newName: "ConversationAttachments");

            migrationBuilder.RenameTable(
                name: "Conversation",
                newName: "Conversations");

            migrationBuilder.RenameIndex(
                name: "IX_ConversationParticipant_ConversationId",
                table: "ConversationParticipants",
                newName: "IX_ConversationParticipants_ConversationId");

            migrationBuilder.RenameIndex(
                name: "IX_ConversationAttachment_ConversationId",
                table: "ConversationAttachments",
                newName: "IX_ConversationAttachments_ConversationId");

            migrationBuilder.RenameIndex(
                name: "IX_Conversation_Type",
                table: "Conversations",
                newName: "IX_Conversations_Type");

            migrationBuilder.RenameIndex(
                name: "IX_Conversation_Title",
                table: "Conversations",
                newName: "IX_Conversations_Title");

            migrationBuilder.RenameIndex(
                name: "IX_Conversation_TicketId",
                table: "Conversations",
                newName: "IX_Conversations_TicketId");

            migrationBuilder.RenameIndex(
                name: "IX_Conversation_Id",
                table: "Conversations",
                newName: "IX_Conversations_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConversationParticipants",
                table: "ConversationParticipants",
                columns: new[] { "UserId", "ConversationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConversationAttachments",
                table: "ConversationAttachments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conversations",
                table: "Conversations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationAttachments_Conversations_ConversationId",
                table: "ConversationAttachments",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationParticipants_Conversations_ConversationId",
                table: "ConversationParticipants",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Conversations_Tickets_TicketId",
                table: "Conversations",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversations_ConversationId",
                table: "Messages",
                column: "ConversationId",
                principalTable: "Conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConversationAttachments_Conversations_ConversationId",
                table: "ConversationAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_ConversationParticipants_Conversations_ConversationId",
                table: "ConversationParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_Conversations_Tickets_TicketId",
                table: "Conversations");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversations_ConversationId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conversations",
                table: "Conversations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConversationParticipants",
                table: "ConversationParticipants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConversationAttachments",
                table: "ConversationAttachments");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "Message");

            migrationBuilder.RenameTable(
                name: "Conversations",
                newName: "Conversation");

            migrationBuilder.RenameTable(
                name: "ConversationParticipants",
                newName: "ConversationParticipant");

            migrationBuilder.RenameTable(
                name: "ConversationAttachments",
                newName: "ConversationAttachment");

            migrationBuilder.RenameIndex(
                name: "IX_Conversations_Type",
                table: "Conversation",
                newName: "IX_Conversation_Type");

            migrationBuilder.RenameIndex(
                name: "IX_Conversations_Title",
                table: "Conversation",
                newName: "IX_Conversation_Title");

            migrationBuilder.RenameIndex(
                name: "IX_Conversations_TicketId",
                table: "Conversation",
                newName: "IX_Conversation_TicketId");

            migrationBuilder.RenameIndex(
                name: "IX_Conversations_Id",
                table: "Conversation",
                newName: "IX_Conversation_Id");

            migrationBuilder.RenameIndex(
                name: "IX_ConversationParticipants_ConversationId",
                table: "ConversationParticipant",
                newName: "IX_ConversationParticipant_ConversationId");

            migrationBuilder.RenameIndex(
                name: "IX_ConversationAttachments_ConversationId",
                table: "ConversationAttachment",
                newName: "IX_ConversationAttachment_ConversationId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Message",
                table: "Message",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conversation",
                table: "Conversation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConversationParticipant",
                table: "ConversationParticipant",
                columns: new[] { "UserId", "ConversationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConversationAttachment",
                table: "ConversationAttachment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Conversation_Tickets_TicketId",
                table: "Conversation",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationAttachment_Conversation_ConversationId",
                table: "ConversationAttachment",
                column: "ConversationId",
                principalTable: "Conversation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConversationParticipant_Conversation_ConversationId",
                table: "ConversationParticipant",
                column: "ConversationId",
                principalTable: "Conversation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Conversation_ConversationId",
                table: "Message",
                column: "ConversationId",
                principalTable: "Conversation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
