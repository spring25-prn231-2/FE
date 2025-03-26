/**
* Template Name: NiceAdmin
* Template URL: https://bootstrapmade.com/nice-admin-bootstrap-admin-html-template/
* Updated: Apr 20 2024 with Bootstrap v5.3.3
* Author: BootstrapMade.com
* License: https://bootstrapmade.com/license/
*/

let milestoneModal;
document.addEventListener('DOMContentLoaded', () => {
    // Create a modal instance after the DOM is loaded
    const modalElement = document.getElementById('milestoneModal');
    milestoneModal = new bootstrap.Modal(modalElement, {});
});
async function viewMilestones(proposalId) {
    // Identify the container for this specific proposal
    //const container = document.getElementById(`milestones-container-${proposalId}`);
    const milestoneModalBody = document.getElementById("milestoneModalBody");
    milestoneModalBody.innerHTML = "<p>Loading milestones...</p>";
    milestoneModal.show();

    // Show a loading message/spinner
    //container.innerHTML = "Loading milestones...";


    try {
        // Send GET request to your endpoint (adjust the URL to your routing)
        const response = await fetch(`https://localhost:7225/api/process/proposalId/${proposalId}`, {
            method: 'GET'
        });

        if (response.status === 404) {
            // If 404, that means "No milestones found"
            milestoneModalBody.innerHTML = "This proposal does not have milestones.";
            return;
        }

        if (!response.ok) {
            // Handle other possible errors
            milestoneModalBody.innerHTML = "Error fetching milestones.";
            return;
        }

        // If response is ok, parse JSON
        const data = await response.json();

        // data is presumably an array of milestone objects
        if (!Array.isArray(data) || data.length === 0) {
            milestoneModalBody.innerHTML = "<p>This proposal does not have milestones.</p>";
            return;
        }
        let milestoneHtml = '<div class="milestone-cards-container">';
        data.forEach(milestone => {
            const processId = milestone["id"];
            const status = milestone["status"] ?? "";
            const taskName = milestone["task-name"] ?? "Untitled";
            const taskDescription = milestone["task-description"] ?? "";
            const cost = milestone.cost ?? 0;
            if (status == "Paid") {
                milestoneHtml += `
                        <div class="milestone-card">
                            <h5>${taskName}</h5>
                            <p>${taskDescription}</p>
                            <p><strong>${cost} VND</strong></p>
                            <p style="color: green;">PAID</p>
                        </div>
                    `;
            } else {
            milestoneHtml += `
                        <div class="milestone-card">
                            <h5>${taskName}</h5>
                            <p>${taskDescription}</p>
                            <p><strong>${cost}VND</strong></p>
                            <button class="btn"  style="border: 2px solid black; padding: 10px; border-radius: 5px;" type="button" onclick="saveProcessIdAndRedirect('${processId}')">
                                pay
                            </button>
                        </div>
                    `;
            }
        });
        milestoneHtml += "</div>";

        // Calculate total cost
        const totalCost = data.reduce((acc, m) => acc + (m.cost || 0), 0);
        // Append total cost at the bottom
        milestoneHtml += `<div class="total-cost">Total: $${totalCost}</div>`;
        // Update the modal body
        milestoneModalBody.innerHTML = milestoneHtml;
    } catch (error) {
        // Catch network errors or other issues
        console.error(error);
        milestoneModalBody.innerHTML = "Something went wrong while fetching milestones.";
    }
}

let isAccepting = false;
async function acceptProposal(proposalId) {
    if (isAccepting) return;

    const userConfirmed = confirm("Are you sure you want to accept this proposal?");
    if (!userConfirmed) {
        // If user clicks "Cancel", just return (do nothing)
        return;
    }

    isAccepting = true;

    // Identify the specific button and "Accepted" span
    const button = document.getElementById(`acceptButton-${proposalId}`);
    const acceptedTextSpan = document.getElementById(`acceptedText-${proposalId}`);

    // Show loading state on the button
    button.disabled = true;
    button.innerText = "Accepting...";

    // Build the PATCH endpoint URL
    const requestUrl = `https://localhost:7225/api/proposal/${proposalId}`;

    try {
        const response = await fetch(requestUrl, {
            method: 'PATCH'
        });

        if (response.ok) {
            // The proposal is accepted
            // 1) Hide ALL accept buttons
            const allAcceptButtons = document.querySelectorAll('[id^="acceptButton-"]');
            allAcceptButtons.forEach(btn => {
                btn.style.display = "none";
            });

            // 2) Show "Accepted" text for this specific proposal
            if (acceptedTextSpan) {
                acceptedTextSpan.classList.remove("d-none");
            }
        } else {
            // Something went wrong on the server side
            alert("Failed to accept proposal. Server error.");
            // Reset the button state
            button.disabled = false;
            button.innerText = "Accept";
        }
    } catch (error) {
        console.error(error);
        alert("Network error while accepting proposal.");
        // Reset the button state
        button.disabled = false;
        button.innerText = "Accept";
    } finally {
        isAccepting = false;
    }
}
async function saveProcessIdAndRedirect(processId) {
       window.location.href = "../Payment/PaymentConfirmation?processId=" + processId; 
}